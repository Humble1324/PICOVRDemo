using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerInputDebugger : MonoBehaviour
{

    public void OnAction(InputAction action)
    {
        Debug.Log($"设备: {action.activeControl.name}\n" +
                  $"操作: {action.name}\n" +
                  $"值: {action.ReadValueAsObject()}\n" +
                  $"类型: {action.activeControl.layout}");
    }



}