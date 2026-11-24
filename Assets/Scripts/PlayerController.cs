using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [Tooltip("Velocidad de traslación del personaje.")]
    public float speed = 6f;

    [Tooltip("Suavizado o velocidad de giro hacia la dirección de movimiento.")]
    public float rotationSmooth = 12f;

    [Header("Gravedad y Suelo")]
    [Tooltip("Fuerza de la gravedad aplicada hacia abajo.")]
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 verticalVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // 1. Detección de suelo: resetea la velocidad vertical si ya está en el suelo
        if (controller.isGrounded && verticalVelocity.y < 0f)
        {
            // Pequeña fuerza negativa constante para mantener contacto con el suelo en pendientes o desniveles
            verticalVelocity.y = -2f;
        }

        // 2. Lectura de entrada WASD con Keyboard.current del nuevo Input System
        Vector3 moveDirection = Vector3.zero;

        if (Keyboard.current != null)
        {
            float horizontal = 0f;
            float vertical = 0f;

            if (Keyboard.current.wKey.isPressed) vertical += 1f;
            if (Keyboard.current.sKey.isPressed) vertical -= 1f;
            if (Keyboard.current.aKey.isPressed) horizontal -= 1f;
            if (Keyboard.current.dKey.isPressed) horizontal += 1f;

            moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        }

        // 3. Movimiento y rotación horizontal
        if (moveDirection.sqrMagnitude > 0.001f)
        {
            // Rotación suave mirando hacia la dirección a la que se desplaza
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmooth * Time.deltaTime);

            // Traslación horizontal
            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        // 4. Aplicar aceleración por gravedad y desplazamiento vertical
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }
}
