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
    /// 文本
    /// </summary>
    [Header("输入框")]
    public InputField inputField;

    /// <summary>
    /// 数量文本
    /// </summary>
    public Text countText;

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

    /// <summary>
    /// 事项未完成的颜色
    /// </summary>
    public Color itemColor;

    /// <summary>
    /// 事项完成后的颜色
    /// </summary>
    public Color itemFinishedColor;

    #endregion

    #region private 字段

    /// <summary>
    /// 事项是否显示
    /// </summary>
    bool isItemShow = true;

    /// <summary>
    /// 事项数量
    /// </summary>
    int count;

    /// <summary>
    /// 事项 ID
    /// </summary>
    int id;

    #endregion

    #region 生命周期函数

    void Start()
    {
        addItemButton.onClick.AddListener(delegate { AddItem(); });
        finishedButton.onClick.AddListener(delegate { ShowOrHideFinishedItem(); });
        id = PlayerPrefs.GetInt("ID", 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    #endregion

    #region 自定义函数

    /// <summary>
    /// 添加事项
    /// </summary>
    void AddItem()
    {
        // 实例化事项，并添加到滚动视图中
        GameObject item = Instantiate(itemPrefab, content.transform);
        item.name = "Item";
        item.transform.SetAsFirstSibling();
        id++;
        PlayerPrefs.SetInt("ID", id);

        Text itemText = item.transform.Find("Toggle/Label").GetComponent<Text>();
        itemText.text = inputField.text;
        inputField.text = "";

        Toggle toggleButton = item.transform.Find("Toggle").GetComponent<Toggle>();
        toggleButton.onValueChanged.AddListener(delegate
        {
            if (toggleButton.isOn)
            {
                FinishItem(item);

                // 修改数据
                DataManager.instance.ModifyItemData(id, itemText.text, true);
            }
            else
            {
                RecoverItem(item);

                // 修改数据
                DataManager.instance.ModifyItemData(id, itemText.text, false);
            }
        });

        Button deleteButton = item.transform.Find("DeleteButton").GetComponent<Button>();
        deleteButton.onClick.AddListener(delegate
        {
            DataManager.instance.DeleteItemData(id, itemText.text, false);
            Destroy(item);
        });

        count++;
        countText.text = $"今天 {count} 件事";

        // 添加数据
        DataManager.instance.AddItemData(id, itemText.text, false);
    }

    /// <summary>
    /// 完成事项
    /// </summary>
    void FinishItem(GameObject item)
    {
        item.transform.SetSiblingIndex(finishedButton.transform.GetSiblingIndex());
        item.SetActive(isItemShow);
        item.GetComponent<Image>().color = itemFinishedColor;
    }

    /// <summary>
    /// 恢复事项
    /// </summary>
    void RecoverItem(GameObject item)
    {
        item.transform.SetSiblingIndex(finishedButton.transform.GetSiblingIndex());
        item.GetComponent<Image>().color = itemColor;
    }

    /// <summary>
    /// 显示或隐藏已完成的事项
    /// </summary>
    void ShowOrHideFinishedItem()
    {
        isItemShow = !isItemShow;

        for(int i = finishedButton.transform.GetSiblingIndex() + 1; i < content.transform.childCount; i++)
        {
            content.transform.GetChild(i).gameObject.SetActive(isItemShow);
        }
    }

    #endregion
}
