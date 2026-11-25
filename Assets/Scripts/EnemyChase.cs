using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyChase : MonoBehaviour
{
    [Header("Configuración del Agente")]
    [Tooltip("Velocidad de movimiento del enemigo.")]
    public float speed = 3.5f;

    [Tooltip("Distancia a la que se detiene del jugador.")]
    public float stoppingDistance = 1.5f;

    private NavMeshAgent agent;
    private PlayerController targetPlayer;
    private float updateTimer = 0f;
    private const float UpdateInterval = 0.2f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.speed = speed;
            agent.stoppingDistance = stoppingDistance;
        }

        // Buscar al jugador al empezar sin usar tags
        targetPlayer = Object.FindFirstObjectByType<PlayerController>();
    }

    private void Update()
    {
        // Si el jugador o el agente no existen, salir sin provocar errores
        if (targetPlayer == null || agent == null) return;

        // Actualizar parámetros por si se modifican desde el Inspector en ejecución
        agent.speed = speed;
        agent.stoppingDistance = stoppingDistance;

        // Actualizar el destino cada 0.2 segundos
        updateTimer += Time.deltaTime;
        if (updateTimer >= UpdateInterval)
        {
            updateTimer = 0f;

            if (agent.isActiveAndEnabled && agent.isOnNavMesh)
            {
                agent.SetDestination(targetPlayer.transform.position);
            }
        }
    }
}
