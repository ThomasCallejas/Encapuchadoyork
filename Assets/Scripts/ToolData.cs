using UnityEngine;

[CreateAssetMenu(fileName = "NewTool", menuName = "Game/Tool")]
public class ToolData : ScriptableObject
{
    [Header("Información de la Herramienta")]
    public string toolName = "Nueva Herramienta";
    public float damage = 25f;
    public float attackRange = 1.5f;
    public float cooldown = 0.5f;
    public bool isRanged = false;

    [Header("Visuales")]
    [Tooltip("Prefab del modelo que se mostrará en la mano del jugador.")]
    public GameObject heldPrefab;
}
