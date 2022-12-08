using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System;
using System.Collections.Generic;

/// <summary>
/// 主题管理器类
/// </summary>
public class ThemeManager : Singleton<ThemeManager>
{
    Dictionary<EnumType.ColorTheme, string> colorThemeDic = new Dictionary<EnumType.ColorTheme, string>
    {
        { EnumType.ColorTheme.Grey, "_grey" },
        { EnumType.ColorTheme.Pink, "_pink" },
        { EnumType.ColorTheme.Blue, "_blue" },
        { EnumType.ColorTheme.Green, "_green" },
    };
    EnumType.ColorTheme currentTheme;
    SpriteAtlas atlas;
    Sprite currentPopupStyle, currentButtonStyle, currentBtnAddStyle, currentInputStyle;
    string currentIconStyle;

    void Start()
    {
        atlas = ResManager.Instance.LoadRes<SpriteAtlas>("texture", "UI");
        
        // 默认上次的主题
        string currentColorTheme = PlayerPrefs.GetString("ColorTheme", "Grey");
        currentTheme = (EnumType.ColorTheme)Enum.Parse(typeof(EnumType.ColorTheme), currentColorTheme);
        ChooseColorTheme(currentTheme);
        EventHandler.CallModifyColorThemeEvent(currentColorTheme);
    }

    /// <summary>
    /// 选择主题
    /// </summary>
    /// <param name="colorTheme">颜色主题</param>
    public void ChooseColorTheme(EnumType.ColorTheme colorTheme)
    {
        string colorStr = colorThemeDic[colorTheme];
        currentButtonStyle = atlas.GetSprite("button" + colorStr);
        currentBtnAddStyle = atlas.GetSprite("button_add" + colorStr);
        currentPopupStyle = atlas.GetSprite("popup" + colorStr);
        currentInputStyle = currentButtonStyle;
        currentIconStyle = colorStr;
    }

    public void ChangePopupStyle(Image[] images)
    {
        for(int i = 0; i < images.Length; i++)
        {
            images[i].sprite = currentPopupStyle;
        }
    }

    public void ChangeImageStyle(Image[] images)
    {
        for(int i = 0; i < images.Length; i++)
        {
            images[i].sprite = currentButtonStyle;
        }
    }

    public void ChangeButtonStyle(Button[] buttons)
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            Image img = buttons[i].GetComponent<Image>();
            img.sprite = currentButtonStyle;
        }
    }

    public void ChangeBtnAddStyle(Button button)
    {
        Image img = button.GetComponent<Image>();
        img.sprite = currentBtnAddStyle;
    }

    public void ChangeToggleStyle(Toggle[] toggles)
    {
        for(int i = 0; i < toggles.Length; i++)
        {
            Image img = toggles[i].image;
            img.sprite = currentButtonStyle;
        }
    }

    public void ChangeInputStyle(InputField[] inputs)
    {
        for(int i = 0; i < inputs.Length; i++)
        {
            Image img = inputs[i].GetComponent<Image>();
            img.sprite = currentInputStyle;
        }
    }

    public string GetIconColor()
    {
        return currentIconStyle;
    }
}
