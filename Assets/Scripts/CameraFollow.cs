using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo")]
    [Tooltip("Transform del objetivo a seguir.")]
    public Transform target;

    [Header("Posicionamiento")]
    [Tooltip("Posición relativa de la cámara respecto al jugador.")]
    public Vector3 offset = new Vector3(0f, 9f, -7f);

    [Tooltip("Tiempo de suavizado para el desplazamiento de la cámara.")]
    public float smoothTime = 0.15f;

    [Header("Rotación")]
    [Tooltip("Si está activo, la cámara siempre orientará su vista hacia el jugador.")]
    public bool lookAtTarget = true;

    private Vector3 currentVelocity = Vector3.zero;

    private void Start()
    {
        if (target == null)
        {
            PlayerController player = Object.FindFirstObjectByType<PlayerController>();
            if (player != null)
            {
                target = player.transform;
            }
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Calcular posición deseada y suavizar con SmoothDamp
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // Mirar hacia el jugador con un ligero desplazamiento hacia arriba
        if (lookAtTarget)
        {
            Vector3 lookTarget = target.position + Vector3.up * 1f;
            transform.LookAt(lookTarget);
        }
    }
}
