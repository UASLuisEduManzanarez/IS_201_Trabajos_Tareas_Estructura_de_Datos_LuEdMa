using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        GameTimer.OnTimeChanged += UpdateTimerDisplay;
    }

    void OnDestroy()
    {
        GameTimer.OnTimeChanged -= UpdateTimerDisplay;
    }

    void UpdateTimerDisplay(float timeRemaining)
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = $"Tiempo: {minutes:00}:{seconds:00}";
        
        if (timeRemaining <= 10f)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.white;
        }
    }
}