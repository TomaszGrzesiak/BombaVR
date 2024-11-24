using UnityEngine;

public class OnTheStartOfTheGame : MonoBehaviour
{
    public Transform node1;
    public Transform node2;
    public Transform node3;
    public Transform node4;
    public Transform node5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public float rotationAngle = 45f;
    private bool isRotating = false;
    
    void Start()
    {
        RotateNode(node1);
        RotateNode(node2);
    }

    private System.Collections.IEnumerator RotateNode(Transform node)
    {
        int randomValue = UnityEngine.Random.Range(0, 8); 
        isRotating = true;
        float currentRotation = 0f;
        float rotationSpeed = 360f;
        float targetRotation = rotationAngle * randomValue;

        while (currentRotation < targetRotation)
        {
            float step = rotationSpeed * Time.deltaTime;
            step = Mathf.Min(step, targetRotation - currentRotation);
            transform.Rotate(0, step, 0);
            currentRotation += step;
            yield return null;
        }
        isRotating = false;
    }
}
