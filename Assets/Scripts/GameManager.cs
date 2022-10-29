using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// 游戏管理器类
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public static GameManager instance;

    /// <summary>
    /// 添加事项按钮
    /// </summary>
    [Header("按钮")]
    public Button addItemButton;

    /// <summary>
    /// 查询事项按钮
    /// </summary>
    public Button searchItemButton;

    /// <summary>
    /// 已完成按钮
    /// </summary>
    public Button finishedButton;

    /// <summary>
    /// 概览按钮
    /// </summary>
    public Button mainButton;

    /// <summary>
    /// 设置按钮
    /// </summary>
    public Button settingButton;

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
    /// 设置窗口
    /// </summary>
    public GameObject settingPanel;

    /// <summary>
    /// 修改窗口
    /// </summary>
    public GameObject modifyPanel;

    /// <summary>
    /// 关闭修改窗口按钮
    /// </summary>
    public Button closeButton;

    /// <summary>
    /// 修改窗口的文本输入框
    /// </summary>
    public InputField modifyInputField;

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

    float screenWidth;

    float screenHeight;

    float preScreenWidth = 1080f;

    float factor;

    public GameObject topSection;

    public GameObject middleSection;

    public GameObject bottomSection;

    public GameObject outerContent;

    public bool isResize;

    bool setOnce = true;
    bool setTwoOnce = true;

    void Start()
    {
        addItemButton.onClick.AddListener(delegate { AddItem(); });
        searchItemButton.onClick.AddListener(delegate { SearchItem(); });
        finishedButton.onClick.AddListener(delegate { ShowOrHideFinishedItem(); });
        closeButton.onClick.AddListener(delegate { HideTheModifyTextWindow(); });
        mainButton.onClick.AddListener(delegate { settingPanel.SetActive(false); });
        settingButton.onClick.AddListener(delegate { settingPanel.SetActive(true); });
        id = PlayerPrefs.GetInt("ID", 0);
        ReadDataFromAllItem();

        screenWidth = Screen.width;
        screenHeight = Screen.height;

        if (isResize)
        {
            ChangeScreenSize();
        }
        else
        {
            ChangePreScreenSize();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(isResize)
        {
            if (setTwoOnce)
            {
                ChangeScreenSize();
                setTwoOnce = false;
            }

            // 屏幕宽高发生变化
            if (screenWidth != Screen.width || screenHeight != Screen.height)
            {
                screenWidth = Screen.width;
                screenHeight = Screen.height;
                ChangeScreenSize();
            }
            
            setOnce = true;
        }
        else
        {
            if (setOnce)
            {
                ChangePreScreenSize();
                setOnce = false;
                setTwoOnce = true;
            }
        }
    }

    public void ChangePreScreenSize()
    {
        outerContent.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        outerContent.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        outerContent.GetComponent<RectTransform>().offsetMin = new Vector2(0, -1920f);
        outerContent.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

        topSection.GetComponent<RectTransform>().offsetMin = new Vector2(0, 1550f);
        bottomSection.GetComponent<RectTransform>().offsetMax = new Vector2(0, -1820f);
    }

    public void ChangeScreenSize()
    {
        outerContent.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        outerContent.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        outerContent.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        outerContent.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

        factor = screenWidth / preScreenWidth;
        topSection.GetComponent<RectTransform>().offsetMin = new Vector2(0, screenHeight / factor - 370);
        bottomSection.GetComponent<RectTransform>().offsetMax = new Vector2(0, -(screenHeight / factor - 100));
    }

    /// <summary>
    /// 添加事项
    /// </summary>
    void AddItem()
    {
        if (inputField.text.Equals(""))
        {
            return;
        }

        string currentDate = DateTime.Now.ToString();

        // 实例化事项，并添加到滚动视图中
        GameObject item = Instantiate(itemPrefab, content.transform);
        item.name = "Item";
        item.transform.SetAsFirstSibling();
        id++;
        PlayerPrefs.SetInt("ID", id);

        // 修改事项文本
        Text itemText = item.transform.Find("TextButton/Text").GetComponent<Text>();
        itemText.text = inputField.text;
        inputField.text = "";

        // 修改事项时间文本
        Text itemDateText = item.transform.Find("TextButton/DateText").GetComponent<Text>();
        itemDateText.text = currentDate;

        // 修改事项 ID
        Text idText = item.transform.Find("IDText").GetComponent<Text>();
        idText.text = id.ToString();

        // 修改事项文本按钮
        Button textButton = item.transform.Find("TextButton").GetComponent<Button>();
        textButton.onClick.AddListener(delegate
        {
            ShowTheModifyTextWindow();
            modifyInputField.text = itemText.text;
            modifyInputField.onValueChanged.AddListener(delegate
            {
                itemText.text = modifyInputField.text;
                // 修改数据
                DataManager.Instance.ModifyItemData(int.Parse(idText.text), itemText.text, true,
                    DateTime.Now.ToString(), DateTime.Now.ToString());
            });
            Debug.Log(itemText);
        });

        // 修改开关
        Toggle toggleButton = item.transform.Find("Toggle").GetComponent<Toggle>();
        toggleButton.onValueChanged.AddListener(delegate
        {
            if (toggleButton.isOn)
            {
                FinishItem(item);

                // 修改数据
                DataManager.Instance.ModifyItemData(int.Parse(idText.text), itemText.text, true,
                    DateTime.Now.ToString(), DateTime.Now.ToString());
            }
            else
            {
                RecoverItem(item);

                // 修改数据
                DataManager.Instance.ModifyItemData(int.Parse(idText.text), itemText.text, false,
                    DateTime.Now.ToString(), DateTime.Now.ToString());
            }
        });

        // 修改删除按钮
        Button deleteButton = item.transform.Find("DeleteButton").GetComponent<Button>();
        deleteButton.onClick.AddListener(delegate
        {
            DataManager.Instance.DeleteItemData(int.Parse(idText.text), itemText.text, false);
            
            string str = itemDateText.text.Substring(0, itemDateText.text.Length - 9);

            if (currentDate.Contains(str))
            {
                count--;
                countText.text = $"今天 {count} 件事";
            }

            Destroy(item);
        });

        // 修改事项数量
        count++;
        countText.text = $"今天 {count} 件事";

        // 添加数据
        DataManager.Instance.AddItemData(id, itemText.text, false,
            currentDate, currentDate, "");
    }

    /// <summary>
    /// 查询事项
    /// </summary>
    void SearchItem()
    {
        if (inputField.text.Equals(""))
        {
            foreach(Transform child in content.transform)
            {
                if (child.name.Equals("FinishedButton"))
                {
                    continue;
                }

                child.gameObject.SetActive(true);
            }

            return;
        }

        foreach(Transform child in content.transform)
        {
            if (child.name.Equals("FinishedButton"))
            {
                continue;
            }

            Text itemText = child.transform.Find("TextButton/Text").GetComponent<Text>();

            if (itemText.text.Contains(inputField.text))
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
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

    /// <summary>
    /// 显示修改事项文本窗口
    /// </summary>
    void ShowTheModifyTextWindow()
    {
        modifyPanel.SetActive(true);
    }

    /// <summary>
    /// 隐藏修改事项文本窗口
    /// </summary>
    void HideTheModifyTextWindow()
    {
        modifyPanel.SetActive(false);
        modifyInputField.onValueChanged.RemoveAllListeners();
    }

    /// <summary>
    /// 从 allItem 里读取事项
    /// </summary>
    void ReadDataFromAllItem()
    {
        string now = DateTime.Now.ToString();

        foreach(Item child in DataManager.Instance.allItem.items)
        {
            string str = child.itemCreatedDate.Substring(0, child.itemCreatedDate.Length - 9);

            // 实例化事项，并添加到滚动视图中
            GameObject item = Instantiate(itemPrefab, content.transform);
            item.name = "Item";
            item.transform.SetAsFirstSibling();

            // 修改事项文本
            Text itemText = item.transform.Find("TextButton/Text").GetComponent<Text>();
            itemText.text = child.itemContent;

            // 修改事项时间文本
            Text itemDateText = item.transform.Find("TextButton/DateText").GetComponent<Text>();
            itemDateText.text = child.itemCreatedDate;

            // 修改事项 ID
            Text idText = item.transform.Find("IDText").GetComponent<Text>();
            idText.text = child.itemID.ToString();

            // 修改事项文本按钮
            Button textButton = item.transform.Find("TextButton").GetComponent<Button>();
            textButton.onClick.AddListener(delegate
            {
                ShowTheModifyTextWindow();
                modifyInputField.text = itemText.text;
                modifyInputField.onValueChanged.AddListener(delegate
                {
                    itemText.text = modifyInputField.text;
                    // 修改数据
                    DataManager.Instance.ModifyItemData(int.Parse(idText.text), itemText.text, true,
                        DateTime.Now.ToString(), DateTime.Now.ToString());
                });
            });

            // 修改开关
            Toggle toggleButton = item.transform.Find("Toggle").GetComponent<Toggle>();
            toggleButton.onValueChanged.AddListener(delegate
            {
                if (toggleButton.isOn)
                {
                    FinishItem(item);

                    // 修改数据
                    DataManager.Instance.ModifyItemData(int.Parse(idText.text), itemText.text, true,
                        DateTime.Now.ToString(), DateTime.Now.ToString());
                }
                else
                {
                    RecoverItem(item);

                    // 修改数据
                    DataManager.Instance.ModifyItemData(int.Parse(idText.text), itemText.text, false,
                        DateTime.Now.ToString(), DateTime.Now.ToString());
                }
            });

            toggleButton.isOn = child.isFinished;

            // 修改删除按钮
            Button deleteButton = item.transform.Find("DeleteButton").GetComponent<Button>();
            deleteButton.onClick.AddListener(delegate
            {
                DataManager.Instance.DeleteItemData(int.Parse(idText.text), itemText.text, false);

                if (now.Contains(str))
                {
                    count--;
                    countText.text = $"今天 {count} 件事";
                }
                
                Destroy(item);
            });

            if (now.Contains(str))
            {
                // 修改事项数量
                count++;
                countText.text = $"今天 {count} 件事";
            }
        }
    }
}
