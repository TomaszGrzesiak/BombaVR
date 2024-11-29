using UnityEngine;

public class WrongWire : MonoBehaviour
{
    private Timer timer;
    public GameObject Cut;

    public Outline outline;
    private bool isHovered = false;

    void Start()
    {
        outline.enabled = false;
        isHovered = false;
        timer = FindFirstObjectByType<Timer>();
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
        if (isHovered){
            this.gameObject.SetActive(false);
            Cut.SetActive(true);
            timer.AddStrike();
        };
    }
}
