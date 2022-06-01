using UnityEngine;

public class LaunchPreview : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 dragStartPoint;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Clear()
    {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }

    public void SetStartPoint(Vector3 worldPoint)
    {
        dragStartPoint = worldPoint;
        lineRenderer.SetPosition(0, dragStartPoint);
    }

    public void SetEndPoint(Vector3 worldPoint)
    {
        var pointOffset = worldPoint - dragStartPoint;
        var endPoint = transform.position + pointOffset;

        lineRenderer.SetPosition(1, endPoint);
    }
}
