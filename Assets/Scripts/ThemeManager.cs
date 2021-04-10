using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 主题管理器类
/// </summary>
public class ThemeManager : MonoBehaviour
{
    /// <summary>
    /// 是否打开黑色主题
    /// </summary>
    [Header("主题开关")]
    public bool darkTheme;

    /// <summary>
    /// 黑色主题图片颜色
    /// </summary>
    [Header("颜色")]
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
    /// 事项颜色
    /// </summary>
    public Color itemColor;

    /// <summary>
    /// 事项完成颜色
    /// </summary>
    public Color itemFinishedColor;

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
    /// 事项预制体
    /// </summary>
    public GameObject itemPrefab;

    /// <summary>
    /// 黑色主题事项预制体
    /// </summary>
    public GameObject itemDarkPrefab;

    void Start()
    {
        ChangeTheme();
    }

    
    void Update()
    {
        
    }

    /// <summary>
    /// 改变主题
    /// </summary>
    public void ChangeTheme()
    {
        if (darkTheme)
        {
            Camera.main.backgroundColor = darkImage;
            scrollView.color = darkImage;
            scrollViewInner.color = darkImage;
            mainPanel.color = darkImage;
            titleText.color = darkText;
            countText.color = darkText;

            ColorBlock cb = new ColorBlock();
            cb.normalColor = darkButtonNormal;
            cb.highlightedColor = darkButtonHighlighted;
            cb.pressedColor = darkButtonPress;
            cb.selectedColor = darkButtonSelected;
            cb.colorMultiplier = 1;
            cb.fadeDuration = 0.1f;

            addItemButton.colors = cb;
            searchItemButton.colors = cb;
            finishedButton.colors = cb;
            addItemButton.transform.GetChild(0).GetComponent<Text>().color = darkText;
            searchItemButton.transform.GetChild(0).GetComponent<Text>().color = darkText;
            finishedButton.transform.GetChild(0).GetComponent<Text>().color = darkText;

            inputField.colors = cb;
            inputField.transform.GetChild(0).GetComponent<Text>().color = darkText;
            inputField.transform.GetChild(1).GetComponent<Text>().color = darkText;

            modifyInputField.colors = cb;
            modifyInputField.transform.GetChild(0).GetComponent<Text>().color = darkText;
            modifyInputField.transform.GetChild(1).GetComponent<Text>().color = darkText;

            GameManager.instance.itemPrefab = itemDarkPrefab;
            GameManager.instance.itemColor = itemColor;
            GameManager.instance.itemFinishedColor = itemFinishedColor;
        }
    }
}
