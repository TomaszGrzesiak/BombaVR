using UnityEngine;

public class SpinScript : MonoBehaviour
{
public GameObject ClockwiseButton;
    public GameObject CounterClockwiseButton;
    public float rotationSpeed = 100f;

    public Material statusMaterial;
    public Renderer statusRenderer;

    private bool isClockwisePressed = false;
    private bool isCounterClockwisePressed = false;
    private Camera mainCamera;
        private Timer timer;


    void Start()
    {
        mainCamera = Camera.main; // Cache the reference to the main camera
                timer = FindFirstObjectByType<Timer>();

    }

    void Update()
    {
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            statusRenderer.material = statusMaterial;
            timer.AddCompleted(1f);
        }
    }

    public void NotCounterStart()
    {
        // Cast a ray from the mouse position
        isClockwisePressed = true;
    }
    public void NotCounterStop()
    {
        // Cast a ray from the mouse position
        isClockwisePressed = false;
    }
    public void CounterStart()
    {
        // Cast a ray from the mouse position
        isCounterClockwisePressed = true;
    }
    public void CounterStop()
    {
        // Cast a ray from the mouse position
        isCounterClockwisePressed = false;
    }

    private void RotateMaze(float angle)
    {
        // Rotate the maze around the Z axis
        transform.Rotate(0, 0, angle);
    }
}
