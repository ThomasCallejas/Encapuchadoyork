using UnityEngine;

public class ToolPickup : MonoBehaviour
{
    [Header("Configuración del Pickup")]
    public ToolData tool;

    private void OnTriggerEnter(Collider other)
    {
        PlayerToolHolder holder = other.GetComponentInParent<PlayerToolHolder>();

        if (holder != null)
        {
            holder.Equip(tool);
            Destroy(gameObject);
        }
    }
}
