using UnityEngine;

public class WireStatus : MonoBehaviour
{
        public Material statusMaterial;
    public Renderer statusRenderer;

    public GameObject CutYellow;
    public GameObject CutGreen;


    // Update is called once per frame
    void Update()
    {
        if (CutGreen.activeSelf && CutYellow.activeSelf)
        {
                        statusRenderer.material = statusMaterial;
                        //break;
        }
    }
}
