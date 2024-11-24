using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class NodeScript : MonoBehaviour
{
    public float rotationAngle = 45f;
    private bool isRotating = false;
    public LEDController ledController;

    public void OnSelectEntered(XRBaseInteractor interactor)
    {
        if (!isRotating)
        {
            StartCoroutine(RotateNode());
        }
    }

    private System.Collections.IEnumerator RotateNode()
    {
        isRotating = true;

        float currentRotation = 0f;
        float rotationSpeed = 360f;
        float targetRotation = rotationAngle;

        while (currentRotation < targetRotation)
        {
            float step = rotationSpeed * Time.deltaTime;
            step = Mathf.Min(step, targetRotation - currentRotation);
            transform.Rotate(0, step, 0);
            currentRotation += step;
            yield return null;
        }

        isRotating = false;
        
        ledController.checkCircuit();
    }
}
