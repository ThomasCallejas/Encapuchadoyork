using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Configuración de Vida")]
    [Tooltip("Vida máxima del objeto.")]
    public float maxHealth = 100f;

    [Tooltip("Si está marcado, destruye el GameObject automáticamente al morir.")]
    public bool destroyOnDeath = true;

    [Header("Eventos")]
    public UnityEvent onDamaged = new UnityEvent();
    public UnityEvent onDeath = new UnityEvent();

    public float currentHealth { get; private set; }
    public bool isDead { get; private set; }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (currentHealth < 0f)
        {
            currentHealth = 0f;
        }

        Debug.Log($"[{gameObject.name}] recibió {amount} de daño. Vida restante: {currentHealth}");

        onDamaged?.Invoke();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        onDeath?.Invoke();

        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }
}
