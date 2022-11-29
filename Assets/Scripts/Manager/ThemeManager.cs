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

    #region 白色主题配色

    /// <summary>
    /// 白色主题图片颜色
    /// </summary>
    [Header("白色主题颜色")]
    public Color whiteImage;

    /// <summary>
    /// 白色主题字体颜色
    /// </summary>
    public Color whiteText;

    /// <summary>
    /// 白色主题按钮普通颜色
    /// </summary>
    public Color whiteButtonNormal;

    /// <summary>
    /// 白色主题按钮高亮颜色
    /// </summary>
    public Color whiteButtonHighlighted;

    /// <summary>
    /// 白色主题按钮按下颜色
    /// </summary>
    public Color whiteButtonPress;

    /// <summary>
    /// 白色主题按钮选中颜色
    /// </summary>
    public Color whiteButtonSelected;

    /// <summary>
    /// 白色主题事项颜色
    /// </summary>
    public Color whiteItemColor;

    /// <summary>
    /// 白色主题事项完成颜色
    /// </summary>
    public Color whiteItemFinishedColor;

    /// <summary>
    /// 白色主题事项预制体
    /// </summary>
    public GameObject whiteItemPrefab;

    #endregion

    #region 黑色主题配色

    /// <summary>
    /// 黑色主题图片颜色
    /// </summary>
    [Header("黑色主题颜色")]
    public Color darkImage;

    /// <summary>
    /// 黑色主题字体颜色
    /// </summary>
    public Color darkText;

    /// <summary>
    /// 黑色主题按钮普通颜色
    /// </summary>
    public Color darkButtonNormal;

    /// <summary>
    /// 黑色主题按钮高亮颜色
    /// </summary>
    public Color darkButtonHighlighted;

    /// <summary>
    /// 黑色主题按钮按下颜色
    /// </summary>
    public Color darkButtonPress;

    /// <summary>
    /// 黑色主题按钮选中颜色
    /// </summary>
    public Color darkButtonSelected;

    /// <summary>
    /// 黑色主题事项颜色
    /// </summary>
    public Color darkItemColor;

    /// <summary>
    /// 黑色主题事项完成颜色
    /// </summary>
    public Color darkItemFinishedColor;

    /// <summary>
    /// 黑色主题事项预制体
    /// </summary>
    public GameObject darkItemPrefab;

    #endregion

    #region 蓝色主题配色

    /// <summary>
    /// 蓝色主题图片颜色
    /// </summary>
    [Header("蓝色主题颜色")]
    public Color blueImage;

    /// <summary>
    /// 蓝色主题字体颜色
    /// </summary>
    public Color blueText;

    /// <summary>
    /// 蓝色主题按钮普通颜色
    /// </summary>
    public Color blueButtonNormal;

    /// <summary>
    /// 蓝色主题按钮高亮颜色
    /// </summary>
    public Color blueButtonHighlighted;

    /// <summary>
    /// 蓝色主题按钮按下颜色
    /// </summary>
    public Color blueButtonPress;

    /// <summary>
    /// 蓝色主题按钮选中颜色
    /// </summary>
    public Color blueButtonSelected;

    /// <summary>
    /// 蓝色主题事项颜色
    /// </summary>
    public Color blueItemColor;

    /// <summary>
    /// 蓝色主题事项完成颜色
    /// </summary>
    public Color blueItemFinishedColor;

    /// <summary>
    /// 蓝色主题事项预制体
    /// </summary>
    public GameObject blueItemPrefab;

    #endregion

    #region 粉色主题配色

    /// <summary>
    /// 粉色主题图片颜色
    /// </summary>
    [Header("粉色主题颜色")]
    public Color pinkImage;

    /// <summary>
    /// 粉色主题字体颜色
    /// </summary>
    public Color pinkText;

    /// <summary>
    /// 粉色主题按钮普通颜色
    /// </summary>
    public Color pinkButtonNormal;

    /// <summary>
    /// 粉色主题按钮高亮颜色
    /// </summary>
    public Color pinkButtonHighlighted;

    /// <summary>
    /// 粉色主题按钮按下颜色
    /// </summary>
    public Color pinkButtonPress;

    /// <summary>
    /// 粉色主题按钮选中颜色
    /// </summary>
    public Color pinkButtonSelected;

    /// <summary>
    /// 粉色主题事项颜色
    /// </summary>
    public Color pinkItemColor;

    /// <summary>
    /// 粉色主题事项完成颜色
    /// </summary>
    public Color pinkItemFinishedColor;

    /// <summary>
    /// 粉色主题事项预制体
    /// </summary>
    public GameObject pinkItemPrefab;

    #endregion

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
