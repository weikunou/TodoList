using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PnlMain : MonoBehaviour
{
    Text textToday;
    Button btnAdd, btnSettings, btnFinished, btnMain, btnSelf;
    InputField modifyInputField;

    Transform content;
    GameObject itemPrefab;
    GameObject modifyPanel;

    int id;
    int count;
    bool isItemShow = true;

    /// <summary>
    /// 事项未完成的颜色
    /// </summary>
    public Color itemColor;

    /// <summary>
    /// 事项完成后的颜色
    /// </summary>
    public Color itemFinishedColor;

    private void Awake()
    {
        textToday = transform.Find("TopSection/TextToday").GetComponent<Text>();

        
        btnSettings = transform.Find("TopSection/BtnSettings").GetComponent<Button>();
        btnFinished = transform.Find("MiddleSection/Scroll View/Viewport/Content/BtnFinished").GetComponent<Button>();

        btnMain = transform.Find("BottomSection/BtnMain").GetComponent<Button>();
        btnAdd = transform.Find("BottomSection/BtnAdd").GetComponent<Button>();
        btnSelf = transform.Find("BottomSection/BtnSelf").GetComponent<Button>();

        modifyInputField = transform.Find("MiddleSection/ModifyPanel/InputField").GetComponent<InputField>();
        modifyPanel = transform.Find("MiddleSection/ModifyPanel").gameObject;

        content = transform.Find("MiddleSection/Scroll View/Viewport/Content");
    }

    private void Start()
    {
        btnAdd.onClick.AddListener(()=>{ OpenNewItem(); });
        btnSettings.onClick.AddListener(()=>{ OpenSettings(); });
        btnFinished.onClick.AddListener(()=>{ ShowOrHideFinishedItem(); });
        btnMain.onClick.AddListener(()=>{ ChangePanel(EnumType.UIPanel.PnlMain); });
        btnSelf.onClick.AddListener(()=>{ ChangePanel(EnumType.UIPanel.PnlSelf); });

        itemPrefab = ResManager.Instance.LoadRes<GameObject>("ui", "Item");
        id = PlayerPrefs.GetInt("ID", 0);
    }

    private void OnEnable()
    {
        EventHandler.AddNewItemEvent += OnAddNewItemEvent;
    }

    private void OnDisable()
    {
        EventHandler.AddNewItemEvent -= OnAddNewItemEvent;
    }

    private void OpenNewItem()
    {
        UIManager.Instance.CreatePanel(EnumType.UIPanel.PnlNewItem, UIManager.Instance.TopCanvas);
    }

    private void OpenSettings()
    {
        UIManager.Instance.CreatePanel(EnumType.UIPanel.PnlSettings, UIManager.Instance.TopCanvas);
    }

    private void ChangePanel(EnumType.UIPanel panel)
    {

    }

    private void OnAddNewItemEvent(string text)
    {
        string currentDate = DateTime.Now.ToString();

        // 实例化事项，并添加到滚动视图中
        GameObject item = Instantiate(itemPrefab, content);
        item.name = "Item";
        item.transform.SetAsFirstSibling();
        id++;
        PlayerPrefs.SetInt("ID", id);

        // 修改事项文本
        Text itemText = item.transform.Find("TextButton/Text").GetComponent<Text>();
        itemText.text = text;

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
                textToday.text = $"今天 {count} 件事";
            }

            Destroy(item);
        });

        // 修改事项数量
        count++;
        textToday.text = $"今天 {count} 件事";

        // 添加数据
        DataManager.Instance.AddItemData(id, itemText.text, false,
            currentDate, currentDate, "");
    }

    /// <summary>
    /// 完成事项
    /// </summary>
    void FinishItem(GameObject item)
    {
        item.transform.SetSiblingIndex(btnFinished.transform.GetSiblingIndex());
        item.SetActive(isItemShow);
        item.GetComponent<Image>().color = itemFinishedColor;
    }

    /// <summary>
    /// 恢复事项
    /// </summary>
    void RecoverItem(GameObject item)
    {
        item.transform.SetSiblingIndex(btnFinished.transform.GetSiblingIndex());
        item.GetComponent<Image>().color = itemColor;
    }

    /// <summary>
    /// 显示或隐藏已完成的事项
    /// </summary>
    void ShowOrHideFinishedItem()
    {
        isItemShow = !isItemShow;

        for(int i = btnFinished.transform.GetSiblingIndex() + 1; i < content.transform.childCount; i++)
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
}
