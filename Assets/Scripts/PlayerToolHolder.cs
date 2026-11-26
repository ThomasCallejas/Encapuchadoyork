using UnityEngine;

public class PlayerToolHolder : MonoBehaviour
{
    [Header("Herramienta Actual")]
    public ToolData currentTool;

    [Header("Visuales")]
    [Tooltip("Punto de anclaje de la mano donde se instancia la herramienta.")]
    public Transform handSlot;

    private GameObject currentHeldInstance;

    public void Equip(ToolData newTool)
    {
        currentTool = newTool;

        // Si ya hay una herramienta visible previa, destruirla
        if (currentHeldInstance != null)
        {
            Destroy(currentHeldInstance);
            currentHeldInstance = null;
        }

        // Si la nueva herramienta tiene prefab y el slot está asignado, instanciarla
        if (newTool != null && newTool.heldPrefab != null && handSlot != null)
        {
            currentHeldInstance = Instantiate(newTool.heldPrefab, handSlot);
            currentHeldInstance.transform.localPosition = Vector3.zero;
            currentHeldInstance.transform.localRotation = Quaternion.identity;
        }

        if (newTool != null)
        {
            Debug.Log($"Herramienta equipada: {newTool.toolName}");
        }
        else
        {
            Debug.Log("Herramienta desequipada.");
        }
    }
}
