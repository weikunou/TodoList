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
    /// 文本输入框
    /// </summary>
    public InputField inputField;

    /// <summary>
    /// 滚动视图的内容
    /// </summary>
    public GameObject content;

    /// <summary>
    /// 事项预制体
    /// </summary>
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
        Text itemText = item.transform.Find("Text").GetComponent<Text>();
        itemText.text = inputField.text;
    }

    #endregion
}
