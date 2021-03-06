using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private bool zoom = true;
    [SerializeField] private float minZoomLimit = 5f;
    [SerializeField] private Camera orthoCamera;

    void LateUpdate()
    {
        float maxOffCenter = 0;
        if (zoom) {
            maxOffCenter = Mathf.Max(Mathf.Abs(player1.position.y), Mathf.Abs(player2.position.y));
            orthoCamera.orthographicSize = Mathf.Max(maxOffCenter, minZoomLimit);
        }
        Vector3 center = new Vector3((player1.position.x + player2.position.x) / 2, 0, 0);
        Vector3 targetPosition = center + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
        
    }
}
