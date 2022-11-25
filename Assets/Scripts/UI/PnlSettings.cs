using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PnlSettings : MonoBehaviour
{
    Button btnClsoe;

    Transform colorGroup, frameGroup;

    private void Awake()
    {
        btnClsoe = transform.Find("Popup/BtnClose").GetComponent<Button>();

        colorGroup = transform.Find("Popup/Scroll View/Viewport/Content/ColorGroup");
        frameGroup = transform.Find("Popup/Scroll View/Viewport/Content/FrameGroup");
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
    
        for(int i = 0; i < frameGroup.childCount; i++)
        {
            if(frameGroup.GetChild(i).TryGetComponent<Toggle>(out Toggle toggle))
            {
                toggle.onValueChanged.AddListener((isOn)=>{ if(isOn) FrameToggleValueChanged(toggle); });
            }
        }

        frameGroup.transform.Find(PlayerPrefs.GetString("Frame", "Default") + "Toggle").GetComponent<Toggle>().isOn = true;
    }

    private void ColorToggleValueChanged(Toggle toggle)
    {
        string colorStr = toggle.name.Split('T')[0];
        ThemeManager.Instance.ChooseColorTheme((ColorTheme)Enum.Parse(typeof(ColorTheme), colorStr));
        PlayerPrefs.SetString("ColorTheme", colorStr);
    }

    private void FrameToggleValueChanged(Toggle toggle)
    {
        string frameStr = toggle.name.Split('T')[0];
        PlayerPrefs.SetString("Frame", frameStr);
        GameManager.Instance.ChangeFrame();
    }

    private void ClosePanel()
    {
        UIManager.Instance.DestroyPanel(EnumType.UIPanel.PnlSettings);
    }
}
