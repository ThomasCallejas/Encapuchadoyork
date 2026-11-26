using UnityEngine;

public class PlayerToolHolder : MonoBehaviour
{
    [Header("Herramienta Actual")]
    public ToolData currentTool;

    public void Equip(ToolData newTool)
    {
        currentTool = newTool;

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
