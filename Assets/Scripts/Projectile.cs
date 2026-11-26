using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Configuración del Proyectil")]
    public float speed = 15f;
    public float damage = 20f;
    public float lifetime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void Setup(float newDamage, float newSpeed)
    {
        damage = newDamage;
        speed = newSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignorar colisiones con el jugador
        if (other.GetComponentInParent<PlayerController>() != null)
        {
            return;
        }

        // Si impacta contra un objetivo con Health que no sea el jugador
        Health targetHealth = other.GetComponentInParent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // Si choca contra cualquier otro collider sólido (paredes, suelo, obstáculos)
        if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
