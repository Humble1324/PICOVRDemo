using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class BaseUI : MonoBehaviour,IView
{
    protected CanvasGroup _canvasGroup;
    
    protected virtual void Awake()
    {
        Init();
        _canvasGroup = GetComponent<CanvasGroup>();
        if (_canvasGroup == null)
        {
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    protected virtual void Start()
    {
        AfterInit();
    }

    private void OnEnable()
    {
        AfterShow();
    }
    public void OnDisable()
    {
        AfterHide(); 
    }
    private void OnDestroy()
    {
        AfterClose();
        Release();
    }



    public abstract void Init();

    public abstract void AfterInit();
    
 
    public abstract void AfterShow();

    public abstract void AfterHide();
    
    public abstract void AfterClose();
    public abstract void Release();
}