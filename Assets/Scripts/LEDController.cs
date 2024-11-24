using UnityEngine;

public class LEDController : MonoBehaviour
{
    public Transform node1;
    public Transform node2;
    public Transform node3;
    public Transform node4;
    public Transform node5;
    private short[] nodes = {0,0,0,0,0};
    
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
    
    public void checkCircuit()
    {
        if (node1.gameObject.transform.rotation.x == 270)
        {
            Debug.Log(("First node set correctly"));
            nodes[0] = 1;
        }

        if (node2.gameObject.transform.rotation.x == 90)
        {
            Debug.Log(("Second node set correctly"));
            nodes[1] = 1;
        }

        if (node3.gameObject.transform.rotation.x == 135)
        {
            Debug.Log(("Third node set correctly"));
            nodes[2] = 1;
        }

        if (node4.gameObject.transform.rotation.x == 270)
        {
            Debug.Log(("Forth node set correctly"));
            nodes[3] = 1;
        }

        if (node5.gameObject.transform.rotation.x == 135)
        {
            Debug.Log(("Fifth node set correctly"));
            nodes[4] = 1;
        }
        
        bool circuitIsCorrect= true;
        foreach (var node in nodes) if (node == 0) circuitIsCorrect = false;
        if (circuitIsCorrect) TurnOn();
            else TurnOff();
    }
}
