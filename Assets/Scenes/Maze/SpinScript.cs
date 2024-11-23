using UnityEngine;

public class SpinScript : MonoBehaviour
{
public GameObject ClockwiseButton;
    public GameObject CounterClockwiseButton;
    public float rotationSpeed = 100f;

    private bool isClockwisePressed = false;
    private bool isCounterClockwisePressed = false;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Cache the reference to the main camera
    }

    void Update()
    {
        // Handle mouse input
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            HandleMouseClick();
        }
        else if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            isClockwisePressed = false;
            isCounterClockwisePressed = false;
        }

        // Rotate the maze if a button is pressed
        if (isClockwisePressed)
        {
            RotateMaze(-rotationSpeed * Time.deltaTime); // Clockwise rotation
        }
        if (isCounterClockwisePressed)
        {
            RotateMaze(rotationSpeed * Time.deltaTime); // Counter-clockwise rotation
        }
    }

    private void HandleMouseClick()
    {
        // Cast a ray from the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Check if the ray hit a button
            if (hit.collider.gameObject == ClockwiseButton)
            {
                isClockwisePressed = true;
            }
            else if (hit.collider.gameObject == CounterClockwiseButton)
            {
                isCounterClockwisePressed = true;
            }
        }
    }

    private void RotateMaze(float angle)
    {
        // Rotate the maze around the Z axis
        transform.Rotate(0, 0, angle);
    }
}
