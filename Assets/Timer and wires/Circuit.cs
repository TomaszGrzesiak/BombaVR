using UnityEngine;

public class Circuit : MonoBehaviour
{
    public GameObject Circuit1;
    public GameObject Circuit2;
    public GameObject Circuit3;
            public Material statusMaterial;
    public Renderer statusRenderer;
    public Timer timer;
    private bool Completed = false;

    void start()
    {
        timer = FindFirstObjectByType<Timer>();
    }

void Update()
{
    if (!Completed)
    {
        //Debug.Log($"Circuit1 local X: {Circuit1.transform.localEulerAngles.x}");
        //Debug.Log($"Circuit2 local X: {Circuit2.transform.localEulerAngles.x}");
        //Debug.Log($"Circuit3 local X: {Circuit3.transform.localEulerAngles.x}");
        if (IsInRotationRange(Circuit1.transform.localEulerAngles.x, 75f, 92f) &&
            IsInRotationRange(Circuit2.transform.localEulerAngles.x, 350f, 3f) &&
            IsInRotationRange(Circuit3.transform.localEulerAngles.x, 77f, 95f))
        {
            timer.AddCompleted(1f);
            statusRenderer.material = statusMaterial;
            Completed = true;
        }
    }


}

    private bool IsInRotationRange(float angle, float min, float max)
    {
        angle = NormalizeAngle(angle);

        if (min > max)
        {
            return angle >= min || angle <= max;
        }
        return angle >= min && angle <= max;
    }

    private float NormalizeAngle(float angle)
    {
        while (angle < 0) angle += 360;
        while (angle >= 360) angle -= 360;
        return angle;
    }
}
