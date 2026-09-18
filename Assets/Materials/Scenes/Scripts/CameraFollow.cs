using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Arrastra tu nave aquí
    public Vector3 offset = new Vector3(0f, 3f, -6f);
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        // Posición deseada detrás de la nave
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        
        // Movimiento suave hacia la posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Hacer que la cámara mire siempre a la nave
        transform.LookAt(target.position + Vector3.up * 1f);
    }
}