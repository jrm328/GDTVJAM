using UnityEngine;
using Unity.Cinemachine;

public class DynamicCameraZoom : MonoBehaviour
{
    public CinemachineCamera virtualCamera;

    [Header("Zoom Settings")]
    public float zoomedInSize = 5f;
    public float zoomedOutSize = 8f;
    public float zoomSmoothTime = 0.5f;

    private bool isMoving;
    private float zoomVelocity = 0f;
    private float currentSize;

    public void SetMoving(bool moving)
    {
        isMoving = moving;
    }

    private void Start()
    {
        if (virtualCamera != null)
        {
            currentSize = virtualCamera.Lens.OrthographicSize;
        }
    }

    private void Update()
    {
        if (virtualCamera == null) return;

        float targetSize = isMoving ? zoomedInSize : zoomedOutSize;

        currentSize = Mathf.SmoothDamp(currentSize, targetSize, ref zoomVelocity, zoomSmoothTime);
        virtualCamera.Lens.OrthographicSize = currentSize;
    }
}
