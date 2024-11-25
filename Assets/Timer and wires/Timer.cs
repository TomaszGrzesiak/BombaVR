using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText; // Reference to the TMP text displaying the time
    public TMP_Text strikeText; // Reference to the TMP text displaying the strikes
    public float startTimeInSeconds = 300; // Default time in seconds (e.g., 5 minutes)

    private float timeRemaining;
    private bool isTimerRunning = false;
    private int strikeCount = 0; // Counter for strikes
    private float TasksCompleted = 0;

    void Start()
    {
        strikeText.text = "";
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

    // Add a strike and handle related logic
    public void AddStrike()
    {
        if (strikeCount < 5)
        {
        strikeCount++;
        Debug.Log($"Strike added! Total strikes: {strikeCount}");
        strikeText.text += "X";

        // Optional: Take an action when strikes reach a certain number
        if (strikeCount >= 3)
        {
            Debug.Log("Three strikes! Stopping the timer.");
            timerText.text = "BOOM";
            StopTimer();
        }
        }
    }

        public void AddCompleted(float add)
    {
        TasksCompleted += add;
        Debug.Log($"Completed a task! Total : {TasksCompleted}");

            if (TasksCompleted >= 6)
            {
                StopTimer();
                timerText.text = "Nice";
            }
    }
}
