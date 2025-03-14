using System;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using CommonUsages = UnityEngine.InputSystem.CommonUsages;
using InputDevice = UnityEngine.InputSystem.InputDevice;


public class PICOInputTest : MonoBehaviour
{
    //Hover 悬停
    //Select 选中
    //Activate 激活
    //Focus 聚焦 ?
    
    [SerializeField] private BaseController leftController;
    [SerializeField] private BaseController rightController;
    public InputActionProperty AButtonAction;
    public InputActionProperty BButtonAction;
    public InputActionProperty XButtonAction;
    public InputActionProperty YButtonAction;
    [Header("物体控制")]
    public GameObject targetObject; // 新增需要控制的物体
    [SerializeField ]private bool isObjectVisible = false; // 物体显示状态标记
    void Start()
    {
        SubscribeControllerEvents(leftController);
        SubscribeControllerEvents(rightController);
    }


    protected void OnEnable()
    {
        AButtonAction.EnableDirectAction();
        BButtonAction.EnableDirectAction();
        XButtonAction.EnableDirectAction(); // 新增启用
        YButtonAction.EnableDirectAction(); // 新增启用
    }

    protected void OnDisable()
    {
        AButtonAction.DisableDirectAction();
        BButtonAction.DisableDirectAction();
        XButtonAction.DisableDirectAction(); // 新增禁用
        YButtonAction.DisableDirectAction(); // 新增禁用
    }

    protected void ReadInput()
    {
        ReadButton(AButtonAction, "A");
        ReadButton(BButtonAction, "B");
        ReadButton(XButtonAction, "X"); // 新增X按钮检测
        ReadButton(YButtonAction, "Y"); // 新增Y按钮检测
    }


    void SubscribeControllerEvents(BaseController controller)
    {
        if (controller == null) return;

        // 订阅所有输入动作
        controller.selectAction.action.performed += ctx => DebugInput(ctx, "Select");
        controller.activateAction.action.performed += ctx => DebugInput(ctx, "Activate");
        controller.uiPressAction.action.performed += ctx => DebugInput(ctx, "UI Press");
        controller.uiScrollAction.action.performed += ctx => DebugInput(ctx, "UI Scroll");
    }

    private void Update()
    {
        ReadInput();
        isObjectVisible=targetObject.activeSelf;
    }
    private void ReadButton(InputActionProperty actionProperty, string buttonName)
    {
        var action = actionProperty.action;
        if (action == null) return;

        var value = action.ReadValue<float>();
        if (value > 0)
        {
            var device = action.activeControl?.device;
            Debug.Log($"[{buttonName}键] 设备: {device?.name} 值: {value}");

            // 新增物体控制逻辑
            switch(buttonName)
            {
                case "X":
                    if(!isObjectVisible)
                    {
                        targetObject.SetActive(true);
                        Debug.Log("物体已显示");
                    }
                    break;
            
                case "B":
                    if(isObjectVisible)
                    {
                        targetObject.SetActive(false);
                        Debug.Log("物体已隐藏");
                    }
                    break;
            }
        }
    }

    void DebugInput(InputAction.CallbackContext context, string actionName)
    {
       // var device = context.control.device;
       // Debug.Log($"[输入检测] 设备: {device.name} " + $"操作: {actionName}\n" + $"值: {context.ReadValueAsObject()}" + $"触发阶段: {context.phase}");
    }
}