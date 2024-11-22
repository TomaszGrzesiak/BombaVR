using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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

    private Material originalMaterial;
    private Renderer wireRenderer;
    public Outline outline;
    private bool isHovered = false;

    void Start()
    {
        outline.enabled = false;
        isHovered = false;
        // Find the Timer script in the scene
        timer = FindFirstObjectByType<Timer>();

        if (timer == null)
        {
            Debug.LogError("Timer script not found in the scene!");
        }

        wireRenderer = GetComponent<Renderer>();
        if (wireRenderer != null)
        {
            originalMaterial = wireRenderer.material;
        }
    }

    public void OnHoverEnter()
    {
        outline.enabled = true;
        isHovered = true;
    }

    public void OnHoverExit()
    {
        outline.enabled = false;
        isHovered = false;
    }

    public void OnSelect()
    {
        if (isHovered) HandleWireCut(Blue);
    }

    void HandleWireCut(GameObject wire)
    {
        if (wire == Blue)
        {
            Blue.SetActive(false);
            BlueCut.SetActive(true);
            timer.AddStrike();
        }
        else if (wire == Yellow)
        {
            Yellow.SetActive(false);
            YellowCut.SetActive(true);
        }
        else if (wire == Red)
        {
            Red.SetActive(false);
            RedCut.SetActive(true);
            timer.AddStrike();
        }
        else if (wire == Green)
        {
            Green.SetActive(false);
            GreenCut.SetActive(true);
        }
    }
}
