using System.Collections;
using UnityEngine;

public class SequencePuzzleManager : MonoBehaviour
{
    public Timer timer;

    public GameObject[] lightObjects;
    public GameObject[] buttonObjects;

    // Sequence of indices to activate (1-based indices)
    public int[] activationSequence = { 5, 3, 7, 1, 1 };

    // Correct answer index (1-based)
    public int correctIndex = 5;

    // Feedback objects
    public GameObject correctFeedbackObject; // Green object
    public GameObject wrongFeedbackObject; // Red object

    // Flag to prevent re-triggering the sequence
    private bool isSequenceActive = false;
    private bool finished = false;
    void Start()
    {
        // Ensure feedback objects are inactive initially
        if (correctFeedbackObject != null)
            correctFeedbackObject.SetActive(false);

        if (wrongFeedbackObject != null)
            wrongFeedbackObject.SetActive(false);
    }

    private IEnumerator ActivateSequence()
    {
        isSequenceActive = true;

        foreach (int index in activationSequence)
        {
            // Validate the index
            if (index > 0 && index <= lightObjects.Length)
            {
                int actualIndex = index - 1; // Convert 1-based to 0-based index
                GameObject lightObject = lightObjects[actualIndex];

                if (lightObject != null)
                {
                    // Activate the light object
                    lightObject.SetActive(true);

                    // Wait for 0.5 seconds
                    yield return new WaitForSeconds(0.5f);

                    // Deactivate the light object
                    lightObject.SetActive(false);
                }
            }
        }

        isSequenceActive = false;
    }

    public void PlaySeq()
    {
            StartCoroutine(ActivateSequence());
    }

    public void CheckAnswer(int button)
    {
        if (button == correctIndex && !finished)
        {
            if (correctFeedbackObject != null)
            correctFeedbackObject.SetActive(true);
            timer.AddCompleted(1f);
            finished = true;
        }
        else
        {
        if (wrongFeedbackObject != null)
        {
            timer.AddStrike();
            wrongFeedbackObject.SetActive(true);
            Invoke(nameof(HideWrongFeedback), 1f);
        }
        }
    }

    private void HideWrongFeedback()
    {
        if (wrongFeedbackObject != null)
            wrongFeedbackObject.SetActive(false);
    }
}
