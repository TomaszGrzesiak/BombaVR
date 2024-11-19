using UnityEngine;

public class ModuleStatus : MonoBehaviour
{
    public Material material1; // First material
    public Material material2; // Second material
    private Renderer objectRenderer;
    private bool isMaterial1Active = true;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        InvokeRepeating("SwitchMaterial", 0f, 1f); // Call SwitchMaterial every second
    }

    void SwitchMaterial()
    {
        if (isMaterial1Active)
        {
            objectRenderer.material = material2;
        }
        else
        {
            objectRenderer.material = material1;
        }

        isMaterial1Active = !isMaterial1Active;
    }
}
