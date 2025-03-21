using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Face : XRBaseInteractable
{
    GameObject _testSpehere;
    public Cube cubeObject;
    public enum FaceType
    {
        Front,
        Back,
        Right,
        Left,
        Top,
        Bottom
    }

    readonly Vector3[] _faces =
    {
        Vector3.forward, Vector3.back,
        Vector3.right, Vector3.left,
        Vector3.up, Vector3.down
    };

    private void Start()
    {
        cubeObject = transform.parent.gameObject.GetComponent<Cube>();
        //朝向就触发
        hoverEntered.AddListener(OnHoverEnterFace);
        hoverExited.AddListener(OnHoverExitFace);
        
        //Grab触发
        selectEntered.AddListener(OnSelectEnterFace);
        selectExited.AddListener(OnSelectExitFace);
        
        //未知
        focusEntered.AddListener(OnFocusEnterFace);
        focusExited.AddListener(OnFocusExitFace);
        
        //需要抓住物体后触发
        activated.AddListener(OnActivatedFace);
        deactivated.AddListener(OnDeactivatedFace);
        
        
        _testSpehere = transform.GetChild(0).gameObject;
    }

    private void OnDeactivatedFace(DeactivateEventArgs arg0)
    {
        //print("Deactivated");
    }

    private void OnActivatedFace(ActivateEventArgs arg0)
    {
        //print("Activated");
        // 获取父物体（当前Cube）的位置和面方向
        Vector3 parentPosition = transform.parent.position;
        Vector3 faceDirection = transform.TransformDirection(_faces[(int)faceType]);
        
        // 在面方向偏移一个单位的位置生成新Cube
        var currentSlot = SlotManager.Instance.currentItemSlot;
        print(currentSlot.itemSlot.stack.id);
        CubeManager.Instance.CreateCube(parentPosition + faceDirection * 1f,currentSlot.itemSlot.stack.id);
    }

    private void OnSelectExitFace(SelectExitEventArgs arg0)
    {
        CubeManager.Instance.onRayDetectedCubeId = -1;
        //print("SelectExit");
    }

    private void OnFocusExitFace(FocusExitEventArgs arg0)
    {
        //print("FocusExit");
    }

    private void OnFocusEnterFace(FocusEnterEventArgs arg0)
    {
        //print("FocusEnter");
    }

    private void OnSelectEnterFace(SelectEnterEventArgs arg0)
    {
        CubeManager.Instance.onRayDetectedCubeId= cubeObject.cubeId;
        //Debug.Log("Select face");
    }

    private void OnHoverExitFace(HoverExitEventArgs arg0)
    {
        //print("HoverExit");
        _testSpehere.SetActive(false);
    }

    private void OnHoverEnterFace(HoverEnterEventArgs arg0)
    {
        //print("HoverEnter");
        #region DebugGizmos

        //Debug.Log(arg0.interactorObject.transform.name);
        // 调用现有方向检测方法
        Vector3 face = _faces[(int)faceType];
        int colorIndex = System.Array.IndexOf(faceColors, face);
        Color lineColor = colorIndex != -1 ? faceColors[colorIndex] : Color.white;
        //Debug.Log(face);
        // 调试可视化
        Debug.DrawRay(transform.position, transform.TransformDirection(face) * 2f, lineColor, 1f);

        #endregion

        _testSpehere.SetActive(true);
    }

    public FaceType faceType;

    Color[] faceColors =
    {
        Color.red, // 前
        Color.cyan, // 后
        Color.green, // 右
        Color.yellow, // 左 
        Color.blue, // 上
        Color.magenta // 下
    };

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = faceColors[(int)faceType];
    //     Gizmos.DrawRay(transform.position, transform.TransformDirection(_faces[(int)faceType]) * 1.5f);
    // }
}