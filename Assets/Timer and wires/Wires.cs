using UnityEngine;

public class Wire : MonoBehaviour
{
    private Timer timer;

    public GameObject Blue;
    public GameObject Yellow;
    public GameObject Red;
    public GameObject Green;

    public GameObject BlueCut;
    public GameObject YellowCut;
    public GameObject RedCut;
    public GameObject GreenCut;

    public GameObject status;
    public Material material1;
    public Material material2;
    public Material material3;
    public Renderer statusObjectRenderer;
    private bool isMaterial1Active = true;

    void Start()
    {
        // Find the Timer script in the scene
        timer = FindFirstObjectByType<Timer>();

        if (timer == null)
        {
            Debug.LogError("Timer script not found in the scene!");
        }
        InvokeRepeating("SwitchMaterial", 0f, 1f); // Call SwitchMaterial every second
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                HandleWireCut(hit.collider.gameObject);
            }
        }
    }

    void HandleWireCut(GameObject wire)
    {
        if (wire == Blue)
        {
            Blue.SetActive(false);
            BlueCut.SetActive(true);
            timer.AddStrike();
            CheckStatus();
        }
        else if (wire == Yellow)
        {
            Yellow.SetActive(false);
            YellowCut.SetActive(true);
            CheckStatus();
        }
        else if (wire == Red)
        {
            Red.SetActive(false);
            RedCut.SetActive(true);
            timer.AddStrike();
            CheckStatus();
        }
        else if (wire == Green)
        {
            Green.SetActive(false);
            GreenCut.SetActive(true);
            CheckStatus();
        }
    }

    void SwitchMaterial()
    {
        if (isMaterial1Active)
        {
            statusObjectRenderer.material = material2;
        }
        else
        {
            statusObjectRenderer.material = material1;
        }

        isMaterial1Active = !isMaterial1Active;
    }

    void CheckStatus()
    {
        if (YellowCut.gameObject.activeSelf && GreenCut.gameObject.activeSelf)
        {
            CancelInvoke("SwitchMaterial");
            statusObjectRenderer.material = material3;
        }
    }
}
