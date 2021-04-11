using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// 颜色主题
/// </summary>
public enum ColorTheme
{
    /// <summary>
    /// 白色主题
    /// </summary>
    White,

    /// <summary>
    /// 黑色主题
    /// </summary>
    Dark,

    /// <summary>
    /// 蓝色主题
    /// </summary>
    Blue,

    /// <summary>
    /// 粉色主题
    /// </summary>
    Pink
}

/// <summary>
/// 主题管理器类
/// </summary>
public class ThemeManager : MonoBehaviour
{
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

    #region 改变颜色的组件

    /// <summary>
    /// 滚动视图
    /// </summary>
    [Header("组件")]
    public Image scrollView;

    /// <summary>
    /// 内层滚动视图
    /// </summary>
    public Image scrollViewInner;

    /// <summary>
    /// 设置窗口滚动视图
    /// </summary>
    public Image scrollViewSetting;

    /// <summary>
    /// 主窗口
    /// </summary>
    public Image mainPanel;

    /// <summary>
    /// 标题文本
    /// </summary>
    public Text titleText;

    /// <summary>
    /// 数量文本
    /// </summary>
    public Text countText;

    /// <summary>
    /// 添加事项按钮
    /// </summary>
    public Button addItemButton;

    /// <summary>
    /// 搜索事项按钮
    /// </summary>
    public Button searchItemButton;

    /// <summary>
    /// 文本输入框
    /// </summary>
    public InputField inputField;

    /// <summary>
    /// 已完成按钮
    /// </summary>
    public Button finishedButton;

    /// <summary>
    /// 修改窗口的文本输入框
    /// </summary>
    public InputField modifyInputField;

    /// <summary>
    /// 设置窗口的标题
    /// </summary>
    public Text settingTitle;

    #endregion

    public ToggleGroup colorToggleGroup;

    public ToggleGroup resizeToggleGroup;

    void Start()
    {
        // 默认黑色主题
        ChooseColorTheme(ColorTheme.Dark);

        var e = new ColorTheme();
        string[] values = Enum.GetNames(e.GetType());

        foreach(Transform child in colorToggleGroup.transform)
        {
            if (child.name.Equals("TitleText"))
            {
                continue;
            }

            child.GetComponent<Toggle>().onValueChanged.AddListener(delegate
            {
                if (child.GetComponent<Toggle>().isOn)
                {
                    string colorStr = child.name.Split('T')[0];

                    ChooseColorTheme((ColorTheme)Enum.Parse(typeof(ColorTheme), colorStr));
                }
            });
        }

        foreach (Transform child in resizeToggleGroup.transform)
        {
            if (child.name.Equals("TitleText"))
            {
                continue;
            }

            child.GetComponent<Toggle>().onValueChanged.AddListener(delegate
            {
                if (child.GetComponent<Toggle>().isOn)
                {
                    string resizeStr = child.name.Split('T')[0];

                    if (resizeStr.Equals("Fix"))
                    {
                        GameManager.instance.isResize = false;
                    }
                    else
                    {
                        GameManager.instance.isResize = true;
                    }
                }
            });
        }
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 选择主题
    /// </summary>
    /// <param name="colorTheme">颜色主题</param>
    public void ChooseColorTheme(ColorTheme colorTheme)
    {
        switch(colorTheme)
        {
            case ColorTheme.White:
                ChangeColorTheme(whiteText, whiteImage,
                    whiteButtonNormal, whiteButtonHighlighted, whiteButtonPress, whiteButtonSelected,
                    whiteItemPrefab, whiteItemColor, whiteItemFinishedColor);
                break;
            case ColorTheme.Dark:
                ChangeColorTheme(darkText, darkImage,
                    darkButtonNormal, darkButtonHighlighted, darkButtonPress, darkButtonSelected,
                    darkItemPrefab, darkItemColor, darkItemFinishedColor);
                break;
            case ColorTheme.Blue:
                ChangeColorTheme(blueText, blueImage,
                    blueButtonNormal, blueButtonHighlighted, blueButtonPress, blueButtonSelected,
                    blueItemPrefab, blueItemColor, blueItemFinishedColor);
                break;
            case ColorTheme.Pink:
                ChangeColorTheme(pinkText, pinkImage,
                    pinkButtonNormal, pinkButtonHighlighted, pinkButtonPress, pinkButtonSelected,
                    pinkItemPrefab, pinkItemColor, pinkItemFinishedColor);
                break;
            default:
                ChangeColorTheme(whiteText, whiteImage,
                    whiteButtonNormal, whiteButtonHighlighted, whiteButtonPress, whiteButtonSelected,
                    whiteItemPrefab, whiteItemColor, whiteItemFinishedColor);
                break;
        }
    }

    /// <summary>
    /// 改变颜色主题
    /// </summary>
    /// <param name="textColor">文本颜色</param>
    /// <param name="imageColor">图片颜色</param>
    /// <param name="buttonNormalColor">按钮正常颜色</param>
    /// <param name="buttonHighlightedColor">按钮高亮颜色</param>
    /// <param name="buttonPressColor">按钮按下颜色</param>
    /// <param name="buttonSelectedColor">按钮选中颜色</param>
    /// <param name="itemPrefab">事项预制体</param>
    /// <param name="itemColor">事项颜色</param>
    /// <param name="itemFinishedColor">事项完成颜色</param>
    public void ChangeColorTheme(Color textColor, Color imageColor,
        Color buttonNormalColor, Color buttonHighlightedColor, Color buttonPressColor, Color buttonSelectedColor,
        GameObject itemPrefab, Color itemColor, Color itemFinishedColor)
    {
        Camera.main.backgroundColor = imageColor;
        scrollView.color = imageColor;
        scrollViewInner.color = imageColor;
        scrollViewSetting.color = imageColor;
        mainPanel.color = imageColor;
        titleText.color = textColor;
        countText.color = textColor;

        ColorBlock cb = new ColorBlock();
        cb.normalColor = buttonNormalColor;
        cb.highlightedColor = buttonHighlightedColor;
        cb.pressedColor = buttonPressColor;
        cb.selectedColor = buttonSelectedColor;
        cb.colorMultiplier = 1;
        cb.fadeDuration = 0.1f;

        addItemButton.colors = cb;
        searchItemButton.colors = cb;
        finishedButton.colors = cb;
        addItemButton.transform.GetChild(0).GetComponent<Text>().color = textColor;
        searchItemButton.transform.GetChild(0).GetComponent<Text>().color = textColor;
        finishedButton.transform.GetChild(0).GetComponent<Text>().color = textColor;

        inputField.colors = cb;
        inputField.transform.Find("Placeholder").GetComponent<Text>().color = textColor;
        inputField.transform.Find("Text").GetComponent<Text>().color = textColor;

        modifyInputField.colors = cb;
        modifyInputField.transform.Find("Placeholder").GetComponent<Text>().color = textColor;
        modifyInputField.transform.Find("Text").GetComponent<Text>().color = textColor;

        GameManager.instance.itemPrefab = itemPrefab;
        GameManager.instance.itemColor = itemColor;
        GameManager.instance.itemFinishedColor = itemFinishedColor;
        GameManager.instance.mainButton.colors = cb;
        GameManager.instance.settingButton.colors = cb;
        GameManager.instance.mainButton.transform.Find("Text").GetComponent<Text>().color = textColor;
        GameManager.instance.settingButton.transform.Find("Text").GetComponent<Text>().color = textColor;
        GameManager.instance.settingPanel.GetComponent<Image>().color = imageColor;

        settingTitle.color = textColor;
        colorToggleGroup.GetComponent<Image>().color = imageColor;
        colorToggleGroup.transform.Find("TitleText").GetComponent<Text>().color = textColor;
        resizeToggleGroup.GetComponent<Image>().color = imageColor;
        resizeToggleGroup.transform.Find("TitleText").GetComponent<Text>().color = textColor;

        foreach (Transform child in GameManager.instance.content.transform)
        {
            if (child.name.Equals("FinishedButton"))
            {
                continue;
            }

            if (child.GetSiblingIndex() < GameManager.instance.finishedButton.transform.GetSiblingIndex())
            {
                child.GetComponent<Image>().color = itemColor;
            }
            else
            {
                child.GetComponent<Image>().color = itemFinishedColor;
            }
            
            child.Find("TextButton/Text").GetComponent<Text>().color = textColor;
            child.Find("Toggle").GetComponent<Toggle>().colors = cb;
            child.Find("DeleteButton").GetComponent<Button>().colors = cb;
            child.Find("DeleteButton/Text").GetComponent<Text>().color = textColor;
        }

        foreach (Transform child in colorToggleGroup.transform)
        {
            if (child.name.Equals("TitleText"))
            {
                continue;
            }

            child.GetComponent<Toggle>().colors = cb;
            child.Find("Label").GetComponent<Text>().color = textColor;
        }

        foreach (Transform child in resizeToggleGroup.transform)
        {
            if (child.name.Equals("TitleText"))
            {
                continue;
            }

            child.GetComponent<Toggle>().colors = cb;
            child.Find("Label").GetComponent<Text>().color = textColor;
        }
    }
}
