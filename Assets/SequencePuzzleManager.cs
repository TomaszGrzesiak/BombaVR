using System.Collections;
using UnityEngine;

public class SequencePuzzleManager : MonoBehaviour
{
    // List of light objects
    public GameObject[] lightObjects;
    public GameObject[] buttonObjects;

    // Sequence of indices to activate (1-based indices)
    public int[] activationSequence = { 5, 3, 7, 1, 1 };

    // Correct answer index (1-based)
    public int correctIndex = 5;

    // Feedback objects
    public GameObject correctFeedbackObject; // Green object
    public GameObject incorrectFeedbackObject; // Red object

    // Flag to prevent re-triggering the sequence
    private bool isSequenceActive = false;
    void Start()
    {
        // Ensure feedback objects are inactive initially
        if (correctFeedbackObject != null)
            correctFeedbackObject.SetActive(false);

        if (incorrectFeedbackObject != null)
            incorrectFeedbackObject.SetActive(false);
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


    // Update is called once per frame
    void Update()
    {
        // Trigger activation sequence with Space key
        if (Input.GetKeyDown(KeyCode.Space) && !isSequenceActive)
        {
            StartCoroutine(ActivateSequence());
        }

        // Handle mouse clicks on objects
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            HandleMouseClick();
        }
    }
    private void HandleMouseClick()
    {
        // Raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject clickedObject = hit.collider.gameObject;

            // Find the clicked object in the light objects
            for (int i = 0; i < buttonObjects.Length; i++)
            {
                if (buttonObjects[i] == clickedObject)
                {
                    if (i + 1 == correctIndex) // Correct index is 1-based
                    {
                        // Activate correct feedback
                        ShowFeedback(correctFeedbackObject);
                    }
                    else
                    {
                        // Activate incorrect feedback
                        ShowFeedback(incorrectFeedbackObject);
                    }
                    break;
                }
            }
        }
    }
    private void ShowFeedback(GameObject feedbackObject)
    {
        if (feedbackObject != null)
        {
            feedbackObject.SetActive(true);
            // Deactivate the feedback object after a short delay
            Invoke(nameof(HideFeedback), 1f);
        }
    }
    private void HideFeedback()
    {
        if (correctFeedbackObject != null)
            correctFeedbackObject.SetActive(false);

        if (incorrectFeedbackObject != null)
            incorrectFeedbackObject.SetActive(false);
    }
}
