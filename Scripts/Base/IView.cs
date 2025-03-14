public interface IView
{
    /// <summary>
    /// 生成初次初始化
    /// </summary>
    void Init();
    /// <summary>
    /// 组件获取、引用绑定等
    /// </summary>
    void AfterInit();
    /// <summary>
    /// 如果有些要在显示后调用就写在这   
    /// </summary>
    void AfterShow();
    /// <summary>
    /// 隐藏之后
    /// </summary>
    void AfterHide();
    /// <summary>
    /// 销毁之前
    /// </summary>
    void AfterClose();
    /// <summary>
    /// 释放资源
    /// </summary>
    void Release();
}