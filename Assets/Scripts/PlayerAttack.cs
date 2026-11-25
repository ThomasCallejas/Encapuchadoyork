using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    [Tooltip("Daño infligido por cada golpe.")]
    public float damage = 25f;

    [Tooltip("Distancia hacia adelante desde el jugador donde se ubica el centro del ataque.")]
    public float attackRange = 1.5f;

    [Tooltip("Radio de la esfera de detección del golpe.")]
    public float attackRadius = 1f;

    [Tooltip("Tiempo de espera mínimo entre ataques en segundos.")]
    public float cooldown = 0.5f;

    private float nextAttackTime = 0f;
    private Health myHealth;

    private void Awake()
    {
        myHealth = GetComponent<Health>();
    }

    private void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextAttackTime)
        {
            Attack();
        }
    }

    private void Attack()
    {
        nextAttackTime = Time.time + cooldown;
        Debug.Log("Ataque");

        Vector3 attackPoint = transform.position + transform.forward * attackRange;
        Collider[] hits = Physics.OverlapSphere(attackPoint, attackRadius);

        HashSet<Health> damagedTargets = new HashSet<Health>();

        foreach (Collider hit in hits)
        {
            // Ignorar al propio jugador
            if (hit.gameObject == gameObject || hit.transform.IsChildOf(transform))
            {
                continue;
            }

            Health targetHealth = hit.GetComponentInParent<Health>();
            if (targetHealth == null)
            {
                targetHealth = hit.GetComponent<Health>();
            }

            // Aplicar daño una sola vez por objetivo en este ataque
            if (targetHealth != null && targetHealth != myHealth && !damagedTargets.Contains(targetHealth))
            {
                damagedTargets.Add(targetHealth);
                targetHealth.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 attackPoint = transform.position + transform.forward * attackRange;
        Gizmos.DrawWireSphere(attackPoint, attackRadius);
    }
}
