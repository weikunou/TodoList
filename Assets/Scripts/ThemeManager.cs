using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    Dark
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
    /// 颜色主题开关
    /// </summary>
    public Toggle colorThemeToggle;

    #endregion

    int colorIndex;

    void Start()
    {
        // 默认黑色主题
        ChooseColorTheme(ColorTheme.Dark);

        colorIndex = 1;

        var e = new ColorTheme();
        string[] values = System.Enum.GetNames(e.GetType());

        colorThemeToggle.onValueChanged.AddListener(delegate
        {
            colorIndex++;

            if(colorIndex >= values.Length)
            {
                colorIndex = 0;
            }

            ChooseColorTheme((ColorTheme)colorIndex);
        });
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

        colorThemeToggle.GetComponent<Toggle>().colors = cb;

        GameManager.instance.itemPrefab = itemPrefab;
        GameManager.instance.itemColor = itemColor;
        GameManager.instance.itemFinishedColor = itemFinishedColor;

        foreach(Transform child in GameManager.instance.content.transform)
        {
            if (child.name.Equals("FinishedButton"))
            {
                continue;
            }

            child.GetComponent<Image>().color = imageColor;
            child.Find("TextButton/Text").GetComponent<Text>().color = textColor;
            child.Find("Toggle").GetComponent<Toggle>().colors = cb;
            child.Find("DeleteButton").GetComponent<Button>().colors = cb;
            child.Find("DeleteButton/Text").GetComponent<Text>().color = textColor;
        }
    }
}
