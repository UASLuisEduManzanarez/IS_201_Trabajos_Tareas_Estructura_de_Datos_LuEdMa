using UnityEngine;
using System;

public class GameTimer : MonoBehaviour
{
    public static Action OnTimeIsUp; 
    public static Action<float> OnTimeChanged; 

    [Header("Tiempo")]
    public float totalTimePerLevel = 120f; 
    

    [HideInInspector] public float currentTime;
    private bool isRunning = false;
    [HideInInspector] public float totalTimePlayed = 0f; 

    void Start()
    {
        GameManager.OnLevelChanged += StartTimer;
        GameManager.OnGameWin += StopTimer;
        GameManager.OnGameOver += StopTimer;
    }

    private void OnDestroy()
    {
        GameManager.OnLevelChanged -= StartTimer;
        GameManager.OnGameWin -= StopTimer;
        GameManager.OnGameOver -= StopTimer;
    }

    public void StartTimer(int level)
    {
        currentTime = totalTimePerLevel; 
        isRunning = true;
        OnTimeChanged?.Invoke(currentTime);
        Debug.Log($"Temporizador iniciado para el Nivel {level}.");
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            totalTimePlayed += Time.deltaTime;
            
            OnTimeChanged?.Invoke(currentTime);

            if (currentTime <= 0)
            {
                currentTime = 0;
                isRunning = false;
                OnTimeChanged?.Invoke(currentTime);
                OnTimeIsUp?.Invoke(); 
            }
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}