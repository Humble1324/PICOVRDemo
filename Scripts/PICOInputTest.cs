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
    [SerializeField ]private bool isObjectVisible = false; // 物体显示状态标记
    void Start()
    {
        //SubscribeControllerEvents(leftController);
        //SubscribeControllerEvents(rightController);
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
    }
    private void ReadButton(InputActionProperty actionProperty, string buttonName)
    {
        var action = actionProperty.action;
        if (action == null) return;
    
        var value = action.ReadValue<float>();
        if (value > 0)
        {
            var device = action.activeControl?.device;
            //Debug.Log($"[{buttonName}键] 设备: {device?.name} 值: {value}");
    
            // 通过UIManager管理UI
            switch(buttonName)
            {
                case "X":
                    UIManager.Instance.ShowUI("UI/SettingPanel"); // 替换为实际的预制体路径
                    //Debug.Log("显示UI面板");
                    break;
        
                case "B":
                    UIManager.Instance.HideUI("UI/SettingPanel"); // 如果需隐藏可添加HideUI方法
                    //Debug.Log("隐藏UI面板");
                    break;
                case "A":
                    //触发删除Cube逻辑
                    print("触发删除Cube逻辑");
                    CubeManager.Instance.TryRemoveCube();
                    break;
            }
        }
    }

    public void DebugButton()
    {
        UIManager.Instance.ShowUI("UI/SettingPanel"); // 替换为实际的预制体路径
    }
    void DebugInput(InputAction.CallbackContext context, string actionName)
    {
       // var device = context.control.device;
       // Debug.Log($"[输入检测] 设备: {device.name} " + $"操作: {actionName}\n" + $"值: {context.ReadValueAsObject()}" + $"触发阶段: {context.phase}");
    }
}