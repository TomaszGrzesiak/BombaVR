using UnityEngine;

public class Handle : MonoBehaviour
{
    public Transform Bomb; // Assign the bomb in the inspector
    private Vector3 initialOffset; // Initial position offset
    private Quaternion initialRotationOffset; // Initial rotation offset

    void Start()
    {
        // Store initial offsets
        initialOffset = Bomb.position - this.transform.position;
        initialRotationOffset = Quaternion.Inverse(this.transform.rotation) * Bomb.rotation;
    }

    void FixedUpdate()
    {
        // Rotate the offset based on the handle's current rotation
        Vector3 adjustedOffset = this.transform.rotation * initialOffset;

        // Update the bomb's position and rotation
        Bomb.position = this.transform.position + adjustedOffset;
        Bomb.rotation = this.transform.rotation * initialRotationOffset;
    }
}
