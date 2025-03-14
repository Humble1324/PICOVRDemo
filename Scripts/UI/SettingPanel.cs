using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : BaseUI
{
    [SerializeField] private float distanceFromCamera = 1.5f; // 面板与相机的距离
    private Canvas _canvas;
    void PositionInFrontOfCamera()
    {
        // 获取XR摄像机
        Camera mainCam =  UIManager.Instance.mainCamera;
        if (mainCam != null)
        {
            // 计算面板位置：摄像机位置 + 前向方向 * 距离
            transform.position = mainCam.transform.position + 
                                mainCam.transform.forward * distanceFromCamera;
            
            // 使面板始终面向摄像机
            transform.LookAt(mainCam.transform);
            transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);
            //print("PositionInFrontOfCamera"+transform.rotation);
        }
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void Init()
    {
        
    }

    public override void AfterInit()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = UIManager.Instance.mainCamera;
    }

    public override void AfterShow()
    {
        print("AfterShow");
        _canvasGroup.alpha = 1;
        PositionInFrontOfCamera();
    }

    public override void AfterHide()
    {
        _canvasGroup.alpha = 0;
    }

    public override void AfterClose()
    {
        
    }

    public override void Release()
    {
        
    }

    // Update is called once per frame

    
}
