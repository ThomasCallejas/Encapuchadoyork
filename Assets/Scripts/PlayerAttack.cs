using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Configuración de Ataque Base (Puños)")]
    [Tooltip("Daño base por defecto si no hay herramienta equipada.")]
    public float damage = 25f;

    [Tooltip("Alcance base hacia adelante si no hay herramienta equipada.")]
    public float attackRange = 1.5f;

    [Tooltip("Radio de la esfera de detección del golpe.")]
    public float attackRadius = 1f;

    [Tooltip("Tiempo de recarga base entre ataques en segundos.")]
    public float cooldown = 0.5f;

    private float nextAttackTime = 0f;
    private Health myHealth;
    private PlayerToolHolder toolHolder;

    private void Awake()
    {
        myHealth = GetComponent<Health>();
        toolHolder = GetComponent<PlayerToolHolder>();
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
        ToolData tool = (toolHolder != null) ? toolHolder.currentTool : null;

        float currentDamage = (tool != null) ? tool.damage : damage;
        float currentRange = (tool != null) ? tool.attackRange : attackRange;
        float currentCooldown = (tool != null) ? tool.cooldown : cooldown;
        string toolName = (tool != null && !string.IsNullOrEmpty(tool.toolName)) ? tool.toolName : "Puños";

        nextAttackTime = Time.time + currentCooldown;
        Debug.Log($"Ataque con {toolName}, daño {currentDamage}");

        Vector3 attackPoint = transform.position + transform.forward * currentRange;
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
                targetHealth.TakeDamage(currentDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        float currentRange = attackRange;
        PlayerToolHolder holder = (toolHolder != null) ? toolHolder : GetComponent<PlayerToolHolder>();
        if (holder != null && holder.currentTool != null)
        {
            currentRange = holder.currentTool.attackRange;
        }

        Vector3 attackPoint = transform.position + transform.forward * currentRange;
        Gizmos.DrawWireSphere(attackPoint, attackRadius);
    }
}
