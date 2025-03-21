using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public Camera mainCamera;
    private void Awake()
    {
        UIManager.Instance = this;
    }

    // 修改字典存储类型为BaseUI
    private Dictionary<string, GameObject> uiInstances = new Dictionary<string, GameObject>();
    
    // 修改后的ShowUI方法
    public void ShowUI(string uiPrefabPath,Transform parent = null)
    {
        if (uiInstances.TryGetValue(uiPrefabPath, out GameObject ui))
        {
            //Debug.Log("Already show UI");
            ui.gameObject.SetActive(true);
            return;
        }

        GameObject prefab = Resources.Load<GameObject>(uiPrefabPath);

        if (prefab != null)
        {
            if (parent == null)
            {
                parent = transform;
            }
            GameObject newUIObj = Instantiate(prefab, parent);
            newUIObj.transform.localScale = Vector3.one;
            print(newUIObj.transform.localScale);
            newUIObj.SetActive(true);

            uiInstances.Add(uiPrefabPath, newUIObj);
        }
    }

    // 修改后的HideUI方法
    public void HideUI(string uiPrefabPath)
    {
        if (uiInstances.TryGetValue(uiPrefabPath, out GameObject ui))
        {
            ui.gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start() { } 
    void Update() { }
}
