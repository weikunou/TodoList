using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏管理器类
/// </summary>
public class GameManager : MonoBehaviour
{
    #region public 字段

    /// <summary>
    /// 添加事项按钮
    /// </summary>
    [Header("按钮")]
    public Button addItemButton;

    /// <summary>
    /// 已完成按钮
    /// </summary>
    public Button finishedButton;

    /// <summary>
    /// 文本输入框
    /// </summary>
    [Header("输入框")]
    public InputField inputField;

    /// <summary>
    /// 滚动视图的内容
    /// </summary>
    [Header("滚动视图")]
    public GameObject content;

    /// <summary>
    /// 事项预制体
    /// </summary>
    [Header("预制体资源")]
    public GameObject itemPrefab;

    #endregion


    #region 生命周期函数

    void Start()
    {
        addItemButton.onClick.AddListener(delegate { AddItem(); });
    }

    void Update()
    {
        
    }

    #endregion

    #region 自定义函数

    /// <summary>
    /// 添加事项
    /// </summary>
    void AddItem()
    {
        GameObject item = Instantiate(itemPrefab, content.transform);
        item.transform.SetAsFirstSibling();

        Text itemText = item.transform.Find("Toggle/Label").GetComponent<Text>();
        itemText.text = inputField.text;

        Toggle toggleButton = item.transform.Find("Toggle").GetComponent<Toggle>();
        toggleButton.onValueChanged.AddListener(delegate { FinishItem(item); });
    }

    /// <summary>
    /// 完成事项
    /// </summary>
    void FinishItem(GameObject item)
    {
        item.transform.SetSiblingIndex(finishedButton.transform.GetSiblingIndex() + 1);
    }

    #endregion
}
