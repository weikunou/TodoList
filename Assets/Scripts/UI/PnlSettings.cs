using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PnlSettings : MonoBehaviour
{
    Image popup, colorGroupPopup, frameGroupPopup;
    Button btnClsoe;

    Transform colorGroup, frameGroup;
    List<Toggle> toggleList = new List<Toggle>();

    private void Awake()
    {
        popup = transform.Find("Popup").GetComponent<Image>();
        colorGroupPopup = transform.Find("Popup/Scroll View/Viewport/Content/ColorGroup").GetComponent<Image>();
        frameGroupPopup = transform.Find("Popup/Scroll View/Viewport/Content/FrameGroup").GetComponent<Image>();

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
                toggleList.Add(toggle);
            }
        }
    
        for(int i = 0; i < frameGroup.childCount; i++)
        {
            if(frameGroup.GetChild(i).TryGetComponent<Toggle>(out Toggle toggle))
            {
                toggle.onValueChanged.AddListener((isOn)=>{ if(isOn) FrameToggleValueChanged(toggle); });
                toggleList.Add(toggle);
            }
        }

        colorGroup.transform.Find(PlayerPrefs.GetString("ColorTheme", "Dark") + "Toggle").GetComponent<Toggle>().isOn = true;
        frameGroup.transform.Find(PlayerPrefs.GetString("Frame", "Default") + "Toggle").GetComponent<Toggle>().isOn = true;
    }

    private void OnEnable()
    {
        EventHandler.ModifyColorThemeEvent += OnModifyColorThemeEvent;
    }

    private void OnDisable()
    {
        EventHandler.ModifyColorThemeEvent -= OnModifyColorThemeEvent;
    }

    private void OnModifyColorThemeEvent(string colorTheme)
    {
        Image[] popups = new Image[]{ popup, colorGroupPopup, frameGroupPopup };
        ThemeManager.Instance.ChangePopupStyle(popups);
        Button[] buttons = new Button[]{ btnClsoe };
        ThemeManager.Instance.ChangeButtonStyle(buttons);
        Toggle[] toggles = toggleList.ToArray();
        ThemeManager.Instance.ChangeToggleStyle(toggles);
    }

    private void ColorToggleValueChanged(Toggle toggle)
    {
        string colorStr = toggle.name.Split('T')[0];
        ThemeManager.Instance.ChooseColorTheme((EnumType.ColorTheme)Enum.Parse(typeof(EnumType.ColorTheme), colorStr));
        EventHandler.CallModifyColorThemeEvent(colorStr);
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
