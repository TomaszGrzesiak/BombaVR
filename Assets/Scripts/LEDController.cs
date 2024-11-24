using UnityEngine;

public class LEDController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public Material ledMaterial; // Assign this in the Inspector
    public Color offColor = Color.black;
    public Color onColor = Color.green;

    public void TurnOn()
    {
        ledMaterial.SetColor("_EmissionColor", onColor * 2f); // Multiply for intensity
        DynamicGI.SetEmissive(GetComponent<Renderer>(), onColor * 2f);
    }

    public void TurnOff()
    {
        ledMaterial.SetColor("_EmissionColor", offColor);
        DynamicGI.SetEmissive(GetComponent<Renderer>(), offColor);
    }
}
