using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Movimiento")]
    public float normalSpeed = 20f;
    public float boostSpeed = 35f;
    public float turnSpeed = 100f;

    [Header("Inclinación (Roll)")]
    public float maxRoll = 35f; 
    public float rollSpeed = 5f;  

    private float currentSpeed;

    void Start()
    {
        currentSpeed = normalSpeed;
    }

    void Update()
    {
        // Activar Turbo con la barra espaciadora
        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed = boostSpeed;
        }
        else
        {
            currentSpeed = normalSpeed;
        }

        // Mover hacia adelante constantemente
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        // Girar a la izquierda o derecha con las flechas o A/D
        float steer = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steer * turnSpeed * Time.deltaTime);

        // Calcular la inclinación lateral (Roll) basada en el giro
        float targetRoll = -steer * maxRoll;
        float currentRoll = Mathf.Lerp(transform.localEulerAngles.z, targetRoll, rollSpeed * Time.deltaTime);

        // Aplicar la rotación manteniendo la dirección actual
        Vector3 currentRotation = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, currentRoll);
    }
}