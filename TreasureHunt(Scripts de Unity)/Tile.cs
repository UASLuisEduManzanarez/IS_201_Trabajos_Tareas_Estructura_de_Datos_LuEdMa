using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector] public int gridX;
    [HideInInspector] public int gridY;
    
    public TileType type = TileType.Empty;
    public WorldItem content; 
    
    public bool isExplored = false; 
    
    private Renderer tileRenderer; 

    void Awake()
    {
        tileRenderer = GetComponent<Renderer>(); 
    }

    public void Init(int x, int y, TileType initialType)
    {
        gridX = x;
        gridY = y;
        type = initialType;
        
        isExplored = false; 
        

        SetVisibility(false); 
        
        if (content != null)
        {
            Destroy(content.gameObject); 
            content = null;
        }
    }
    
    public void SetType(TileType newType)
    {
        type = newType;
    }

    public void SetVisibility(bool visible)
    {
        if (tileRenderer != null)
        {
            tileRenderer.enabled = visible;
        }

        if (visible)
        {
            isExplored = true;
        }

        if (content != null)
        {
            bool finalVisibility = visible; 

            Renderer[] allRenderers = content.gameObject.GetComponentsInChildren<Renderer>(true);
            foreach (Renderer r in allRenderers)
            {
                r.enabled = finalVisibility; 
            }

            Light[] allLights = content.gameObject.GetComponentsInChildren<Light>(true);
            foreach (Light l in allLights)
            {
                l.enabled = finalVisibility;
            }
        }
    }
}