using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<string, GameObject> ui_objects = new Dictionary<string, GameObject>();

    private Transform mainCanvas;

    private Transform topCanvas;

    private Transform content;

    public Transform TopCanvas { get { return topCanvas; } }

    private void Start()
    {
        mainCanvas = transform.Find("MainCanvas");
        topCanvas = transform.Find("TopCanvas");
        content = mainCanvas.Find("Scroll View/Viewport/Content");
    }

    public void CreatePanel(EnumType.UIPanel uiPanel, Transform parent)
    {
        string panelName = Enum.GetName(typeof(EnumType.UIPanel), uiPanel);
        if(!ui_objects.ContainsKey(panelName))
        {
            GameObject obj = ResManager.Instance.LoadRes<GameObject>("ui", panelName);
            GameObject panel = Instantiate(obj, parent);
            ui_objects.Add(panelName, panel);
        }
    }

    public GameObject GetPanel(EnumType.UIPanel uiPanel)
    {
        string panelName = Enum.GetName(typeof(EnumType.UIPanel), uiPanel);
        if(ui_objects.ContainsKey(panelName))
        {
            return ui_objects[panelName];
        }
        return null;
    }

    public void DestroyPanel(EnumType.UIPanel uiPanel)
    {
        string panelName = Enum.GetName(typeof(EnumType.UIPanel), uiPanel);
        GameObject panel = ui_objects[panelName];
        Destroy(panel);
        ui_objects.Remove(panelName);
    }

    public void DestroyAllPanel()
    {
        foreach(var value in ui_objects.Values)
        {
            Destroy(value);
        }
        ui_objects.Clear();
    }
}
