using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<string, GameObject> ui_objects = new Dictionary<string, GameObject>();

    private Transform mainCanvas;

    private Transform topCanvas;

    public Transform MainCanvas { get { return mainCanvas; } }
    public Transform TopCanvas { get { return topCanvas; } }

    private SpriteAtlas atlas;

    private void Start()
    {
        mainCanvas = transform.Find("MainCanvas");
        topCanvas = transform.Find("TopCanvas");

        CreatePanel(EnumType.UIPanel.PnlMain, mainCanvas);

        atlas = ResManager.Instance.LoadRes<SpriteAtlas>("texture", "UI");
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

    public Sprite GetSpriteFromUIAtlas(string name)
    {
        return atlas.GetSprite(name);
    }

    public void SetImage(Image image, string icon)
    {
        image.sprite = GetSpriteFromUIAtlas(icon);
    }

    public void SetImageWithColor(Image image, string icon)
    {
        string color = ThemeManager.Instance.GetIconColor();
        image.sprite = GetSpriteFromUIAtlas(icon + color);
    }
}
