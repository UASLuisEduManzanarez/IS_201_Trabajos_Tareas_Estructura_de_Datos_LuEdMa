using UnityEngine;
using System.Collections; 
using System.Collections.Generic; 
using System; 
using Random = UnityEngine.Random; 
using System.Linq;
using TMPro;

public enum TileType { Empty, Wall, Treasure, Trap, Exit }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // --- EVENTOS DEL JUEGO ---
    public static Action<int> OnLevelChanged;
    public static Action OnGameWin;
    public static Action OnGameOver;

    // --- COMPONENTES Y CONFIGURACIÓN ---
    public CameraController cameraController;
    public int currentLevel = 1;
    public const int MAX_LEVELS = 5;

    [Header("Board settings")]
    public int size = 20;
    public GameObject tilePrefab;
    public Transform boardParent;
    public float tileSpacing = 1f;
    [Header("Prefabs Dictionary")]
    private Dictionary<string, GameObject> prefabLookup;

    [Header("Prefabs")]
    public GameObject trapLifePrefab;
    public GameObject trapEnergyPrefab;
    public GameObject wallPrefab;
    public GameObject exitPrefab;
    public GameObject playerPrefab;

    [Header("Prefabs de Tesoros")]
    public GameObject Treasure_VidaPrefab; 
    public GameObject Treasure_EnergiaPrefab;
    public GameObject Treasure_PuntosPrefab;

    [Header("Distribution")]
    [Range(0f, 1f)] public float treasureDensity = 0.05f;
    [Range(0f, 1f)] public float trapDensity = 0.03f;
    [Range(0f, 1f)] public float wallDensity = 0.25f;

    [Header("UI Popups")]
    public GameObject NotificationPanel;
    public TextMeshProUGUI NotificationText;

    // --- ESTADO DEL JUEGO ---
    public Tile[,] board;
    private PlayerController player;
    public GameTimer gameTimer;
    private Coroutine notificationCoroutine;
    private bool gameIsOver = false;
    private Vector2Int exitPosition = new Vector2Int(-1, -1);
    private List<GameObject> availableTreasures; 
    
    [Header("Audio")]
    private AudioSource gameAudioSource; 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gameTimer = GetComponent<GameTimer>();
        
        gameAudioSource = GetComponent<AudioSource>();
        if (gameAudioSource != null)
        {
            OnLevelChanged += StartGameMusic;
            OnGameOver += StopGameMusic; 
            OnGameWin += StopGameMusic;
        }


        if (gameTimer != null)
        {
            GameTimer.OnTimeIsUp += GameOver;
        }
        OnGameOver += () => gameIsOver = true;
        OnGameWin += () => gameIsOver = true;
        
        InitializePrefabLookup(); 
        InitializeAvailableTreasures();
    }
    
    private void StartGameMusic(int newLevel)
    {
        if (gameAudioSource != null && gameAudioSource.clip != null)
        {
            if (!gameAudioSource.isPlaying)
            {
                gameAudioSource.Play();
            }
        }
    }

    private void StopGameMusic()
    {
        if (gameAudioSource != null && gameAudioSource.isPlaying)
        {
            gameAudioSource.Stop();
        }
    }


    private void InitializeAvailableTreasures()
    {
        availableTreasures = new List<GameObject>
        {
            Treasure_VidaPrefab,
            Treasure_EnergiaPrefab,
            Treasure_PuntosPrefab,
        };
        availableTreasures.RemoveAll(item => item == null);
    }

    private void OnDestroy()
    {
        if (gameTimer != null)
        {
            GameTimer.OnTimeIsUp -= GameOver;
        }
        OnGameOver -= () => gameIsOver = true;
        OnGameWin -= () => gameIsOver = true;

        if (gameAudioSource != null)
        {
            OnLevelChanged -= StartGameMusic;
            OnGameOver -= StopGameMusic;
            OnGameWin -= StopGameMusic;
        }
    }

   void Start()
{
    InitializePrefabLookup();
    gameIsOver = false;
    Time.timeScale = 1f;

    HideNotification();
    
}   void InitializePrefabLookup()
{
    prefabLookup = new Dictionary<string, GameObject>
    {
        { "Treasure_Vida", Treasure_VidaPrefab },
        { "Treasure_Energia", Treasure_EnergiaPrefab },
        { "Treasure_Puntos", Treasure_PuntosPrefab },

    };
}
public GameObject GetPrefabByName(string name)
{
    string cleanedName = CleanItemName(name);

    
    if (prefabLookup.ContainsKey(cleanedName))
    {
        return prefabLookup[cleanedName];
    }
    
    Debug.LogError($"No se encontró el prefab con el nombre: {cleanedName}. La clave original era: {name}");
    return null;
}

    public Vector3 GridToWorld(int x, int y)
    {
        float offset = tileSpacing / 2f;
        return new Vector3(x * tileSpacing + offset, y * -tileSpacing - offset, 0);
    }

    public void ClearBoard()
    {
        if (boardParent != null)
        {
            Destroy(boardParent.gameObject);
        }

        boardParent = new GameObject("Board").transform;

        if (player != null)
        {
            
        }

        board = null;
        exitPosition = new Vector2Int(-1, -1);
    }

    private string CleanItemName(string fullName)
{
    int lastUnderscore = fullName.LastIndexOf('_');
    if (lastUnderscore > 0)
    {
        int secondLastUnderscore = fullName.LastIndexOf('_', lastUnderscore - 1);
        
        if (secondLastUnderscore > 0)
        {
             fullName = fullName.Substring(0, secondLastUnderscore);
        }
    }

    return fullName.Replace("(Clone)", "").Trim();
}
    public void LevelComplete()
    {
        if (currentLevel >= MAX_LEVELS)
        {
            gameTimer?.StopTimer();
            Time.timeScale = 0f;
            OnGameWin?.Invoke();
            return;
        }

        if (exitPosition.x != -1 && board != null &&
            exitPosition.x >= 0 && exitPosition.x < size &&
            exitPosition.y >= 0 && exitPosition.y < size)
        {
            Tile exitTile = board[exitPosition.x, exitPosition.y];
            if (exitTile != null)
            {
                exitTile.isExplored = false;
                exitTile.SetVisibility(false);
            }
        }

        player = FindAnyObjectByType<PlayerController>(); 

        int savedLives = player != null ? player.lives : 3;
        int savedEnergy = player != null ? Mathf.Min(player.energy, 4) : 4;
        int savedScore = player != null ? player.Score : 0;


        List<string> savedInventoryNames = new List<string>();
        if (player != null && player.inventoryItems != null)
        {
            savedInventoryNames = player.inventoryItems
                .Where(item => item != null && item.gameObject != null)
                .Select(item => CleanItemName(item.gameObject.name)) 
            .ToList();
        }

        currentLevel++;
        OnLevelChanged?.Invoke(currentLevel);

        ClearBoard();
        GenerateBoard();


        SpawnPlayerForNewLevel(savedLives, savedEnergy, savedScore, savedInventoryNames);
    }

    public void GameOver()
    {
        gameTimer?.StopTimer();
        Time.timeScale = 0f;
        OnGameOver?.Invoke();
    }

    public void ResetGame()
    {
        currentLevel = 1;
        gameIsOver = false;
        Time.timeScale = 1f;

        ClearBoard();
        GenerateBoard();

        int initialMaxEnergy = 4;

        SpawnPlayerForNewLevel(3, initialMaxEnergy, 0, new List<String>());

        OnLevelChanged?.Invoke(currentLevel);
        gameTimer?.StartTimer(currentLevel);
    }

    public void SpawnPlayerForNewLevel(int startLives, int startEnergy, int startScore, List<String> startInventory)
    {
        int px = 1;
        int py = 1;

        if (player == null)
        {

            Vector3 pos = GridToWorld(px, py);
            GameObject playerGO = Instantiate(playerPrefab, pos, Quaternion.identity);
            player = playerGO.GetComponent<PlayerController>();
        }
        player.gridX = px;
        player.gridY = py;
        player.transform.position = GridToWorld(px, py);

        player.InitializeStats(startLives, startEnergy, startScore, startInventory);

        if (cameraController != null)
        {
            cameraController.target = player.transform;
            cameraController.SnapToTarget();
        }

        GameDataSaver dataSaver = UnityEngine.Object.FindAnyObjectByType<GameDataSaver>();
        if (dataSaver != null)
        {
            dataSaver.SaveGame();
        }

        gameTimer?.StartTimer(currentLevel);
    }

    public void ShowNotification(string message, float duration = 2f)
    {
        if (NotificationPanel != null && NotificationText != null)
        {
            if (notificationCoroutine != null)
            {
                StopCoroutine(notificationCoroutine);
                Time.timeScale = 1f;
            }

            NotificationText.text = message;
            NotificationPanel.SetActive(true);

            notificationCoroutine = StartCoroutine(ShowNotificationCoroutine(duration));
        }
    }

    private IEnumerator ShowNotificationCoroutine(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        HideNotification();
        notificationCoroutine = null;
    }

    public void HideNotification()
    {
        if (NotificationPanel != null)
        {
            NotificationPanel.SetActive(false);

            if (!gameIsOver)
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void GenerateBoard()
    {
        if (boardParent == null) boardParent = new GameObject("Board").transform;

        board = new Tile[size, size];

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                Vector3 pos = GridToWorld(x, y);
                GameObject tileGO = Instantiate(tilePrefab, pos, Quaternion.identity, boardParent);
                tileGO.name = $"Tile_{x}_{y}";

                Tile tile = tileGO.GetComponent<Tile>();
                if (tile == null) tile = tileGO.AddComponent<Tile>();

                tile.Init(x, y, TileType.Empty);
                board[x, y] = tile;
            }
        }

        GenerateMaze();
        PlaceExitFarFromSpawn();

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (board[x, y].type == TileType.Empty)
                {
                    if (x < 5 && y < 5) continue;

                    float r = Random.value;
                    float currentTreasureDensity = treasureDensity;
                    if (availableTreasures == null || availableTreasures.Count == 0)
            {
                InitializeAvailableTreasures();
            }

                    if (r < currentTreasureDensity)
                    {
                      if (availableTreasures != null && availableTreasures.Count > 0)
                {
                    GameObject selectedTreasure = availableTreasures[UnityEngine.Random.Range(0, availableTreasures.Count)];
                    PlacePrefabAt(selectedTreasure, x, y);
                }
                    }
                    else if (r < currentTreasureDensity + trapDensity)
                    {
                        GameObject selectedTrap = null;
                        float trapChoice = Random.value;
                            
                        if (trapChoice < 0.5f)
                        {
                            selectedTrap = trapLifePrefab;
                        }
                        else
                        {
                            selectedTrap = trapEnergyPrefab;
                        }

                        if (selectedTrap != null)
                        {
                            PlacePrefabAt(selectedTrap, x, y);
                        }
                    }
                }
            }
        }
    }

    void PlaceExitFarFromSpawn()
    {
        int ex = 0, ey = 0;
        int maxAttempts = 500; 
        float maxDistance = Mathf.Sqrt(size * size + size * size);
        float minDistance = maxDistance * 0.75f; 

        for (int i = 0; i < maxAttempts; i++)
        {
            ex = Random.Range(1, size - 1);
            ey = Random.Range(1, size - 1);

            float dist = Vector2.Distance(new Vector2(ex, ey), new Vector2(1, 1));

            if (board[ex, ey].type == TileType.Empty && dist > minDistance)
            {
                exitPosition = new Vector2Int(ex, ey);
                PlacePrefabAt(exitPrefab, ex, ey);
                return; 
            }
        }

        for (int x = size - 2; x >= 1; x--)
        {
            for (int y = size - 2; y >= 1; y--)
            {
                if (board[x, y].type == TileType.Empty)
                {
                    exitPosition = new Vector2Int(x, y);
                    PlacePrefabAt(exitPrefab, x, y);
                    return;
                }
            }
        }
    }

    void GenerateMaze()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                board[x, y].SetType(TileType.Wall);

                if (x > 0 && x < size - 1 && y > 0 && y < size - 1)
                {
                    PlacePrefabAt(wallPrefab, x, y);
                }
            }
        }

        int startX = 1;
        int startY = 1;

        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        stack.Push(new Vector2Int(startX, startY));

        RemoveWallAt(startX, startY);

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Peek();
            int cx = current.x;
            int cy = current.y;

            List<Vector2Int> neighbors = GetUnvisitedNeighbors(cx, cy);

            if (neighbors.Count > 0)
            {
                Vector2Int next = neighbors[Random.Range(0, neighbors.Count)];
                int nx = next.x;
                int ny = next.y;

                int wallX = cx + (nx - cx) / 2;
                int wallY = cy + (ny - cy) / 2;

                RemoveWallAt(nx, ny);
                RemoveWallAt(wallX, wallY);

                stack.Push(next);
            }
            else
            {
                stack.Pop();
            }
        }

        for (int i = 0; i < size; i++)
        {
            RemoveWallAt(i, size - 1);
            RemoveWallAt(i, 0);
            RemoveWallAt(0, i);
            RemoveWallAt(size - 1, i);
        }
    }

    void RemoveWallAt(int x, int y)
    {
        if (x >= 0 && x < size && y >= 0 && y < size)
        {
            board[x, y].SetType(TileType.Empty);

            if (board[x, y].content != null)
            {
                Destroy(board[x, y].content.gameObject);
                board[x, y].content = null;
            }
        }
    }

    List<Vector2Int> GetUnvisitedNeighbors(int x, int y)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 2), new Vector2Int(0, -2),
            new Vector2Int(2, 0), new Vector2Int(-2, 0)
        };

        foreach (var dir in directions)
        {
            int nx = x + dir.x;
            int ny = y + dir.y;

            if (nx > 0 && nx < size - 1 && ny > 0 && ny < size - 1 && board[nx, ny].type == TileType.Wall)
            {
                neighbors.Add(new Vector2Int(nx, ny));
            }
        }
        return neighbors;
    }

    public void RevealArea(int centerX, int centerY, int radius)
    {
        for (int x = centerX - radius; x <= centerX + radius; x++)
        {
            for (int y = centerY - radius; y <= centerY + radius; y++)
            {
                if (x >= 0 && x < size && y >= 0 && y < size)
                {
                    board[x, y].SetVisibility(true);
                }
            }
        }
    }

    void PlacePrefabAt(GameObject prefab, int x, int y)
    {
        if (prefab == null) return;
        if (x < 0 || x >= size || y < 0 || y >= size) return;

        Tile t = board[x, y];
        if (t == null) return;

        if (t.content != null && prefab != exitPrefab) return;
        if (t.content != null && prefab == exitPrefab) Destroy(t.content.gameObject);

        Vector3 pos = GridToWorld(x, y);
        GameObject go = Instantiate(prefab, pos, Quaternion.identity, boardParent);
        go.name = $"{prefab.name}_{x}_{y}";

        WorldItem contentComponent = go.GetComponent<WorldItem>();

        if (contentComponent == null)
        {
            Debug.LogError($"Error: El prefab {prefab.name} en ({x},{y}) no tiene un script WorldItem o derivado.");
            Destroy(go);
            return;
        }

        t.content = contentComponent;
        if (prefab == Treasure_PuntosPrefab || prefab == Treasure_VidaPrefab || prefab == Treasure_EnergiaPrefab)
        {
            t.SetType(TileType.Treasure);
        }
        else if (prefab == trapLifePrefab || prefab == trapEnergyPrefab) 
        {
            t.SetType(TileType.Trap);
        }
        else if (prefab == wallPrefab)
        {
            t.SetType(TileType.Wall);
        }
        else if (prefab == exitPrefab)
        {
            t.SetType(TileType.Exit);
        }

        t.SetVisibility(t.isExplored);
    }
}