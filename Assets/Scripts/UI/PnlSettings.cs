using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PnlSettings : MonoBehaviour
{
    Button btnClsoe;

    Transform colorGroup, resizeGroup;

    private void Awake()
    {
        btnClsoe = transform.Find("Popup/BtnClose").GetComponent<Button>();

        colorGroup = transform.Find("Popup/Scroll View/Viewport/Content/ColorGroup");
        resizeGroup = transform.Find("Popup/Scroll View/Viewport/Content/ResizeGroup");
    }

    private void Start()
    {
        btnClsoe.onClick.AddListener(()=>{ ClosePanel(); });

        for(int i = 0; i < colorGroup.childCount; i++)
        {
            if(colorGroup.GetChild(i).TryGetComponent<Toggle>(out Toggle toggle))
            {
                toggle.onValueChanged.AddListener((isOn)=>{ if(isOn) ColorToggleValueChanged(toggle); });
            }
        }

        colorGroup.transform.Find(PlayerPrefs.GetString("ColorTheme", "Dark") + "Toggle").GetComponent<Toggle>().isOn = true;

        for(int i = 0; i < resizeGroup.childCount; i++)
        {
            if(resizeGroup.GetChild(i).TryGetComponent<Toggle>(out Toggle toggle))
            {
                toggle.onValueChanged.AddListener((isOn)=>{ if(isOn) ResizeToggleValueChanged(toggle); });
            }
        }

        resizeGroup.transform.Find(PlayerPrefs.GetString("ResizePlan", "Fix") + "Toggle").GetComponent<Toggle>().isOn = true;
    }

    private void ColorToggleValueChanged(Toggle toggle)
    {
        string colorStr = toggle.name.Split('T')[0];
        ThemeManager.Instance.ChooseColorTheme((ColorTheme)Enum.Parse(typeof(ColorTheme), colorStr));
        PlayerPrefs.SetString("ColorTheme", colorStr);
    }

    private void ResizeToggleValueChanged(Toggle toggle)
    {
        string resizeStr = toggle.name.Split('T')[0];

        if (resizeStr.Equals("Fix"))
        {
            GameManager.Instance.isResize = false;
        }
        else
        {
            GameManager.Instance.isResize = true;
        }

        PlayerPrefs.SetString("ResizePlan", resizeStr);
    }

    private void ClosePanel()
    {
        UIManager.Instance.DestroyPanel(EnumType.UIPanel.PnlSettings);
    }
}
