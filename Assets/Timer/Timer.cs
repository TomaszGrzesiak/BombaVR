using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText; // Reference to the TMP text displaying the time
    public float startTimeInSeconds = 300; // Default time in seconds (e.g., 5 minutes)

    private float timeRemaining;
    private bool isTimerRunning = false;

    void Start()
    {
        SetTime(startTimeInSeconds);
        StartTimer();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                isTimerRunning = false;
                UpdateTimerDisplay();
                TimerEnded();
            }
        }
    }

    public void SetTime(float timeInSeconds)
    {
        timeRemaining = timeInSeconds;
        UpdateTimerDisplay();
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void TimerEnded()
    {
        Debug.Log("Timer has ended!");
        // Add additional logic when the timer ends, if needed.
    }
}
