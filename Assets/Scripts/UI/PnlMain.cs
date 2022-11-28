using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PnlMain : MonoBehaviour
{
    Text textHistory, textToday, textNotFinished;
    Button btnAdd, btnSettings, btnFinished, btnMain, btnSelf;
    Image imgMain, imgSelf, bottomSection;

    Transform content;
    GameObject itemPrefab;
    GameObject selectedItem;

    int id;
    int count;
    bool isItemShow = true;
    EnumType.UIPanel currentPanel = EnumType.UIPanel.PnlMain;

    private void Awake()
    {
        textHistory = transform.Find("TopSection/TextGroup/TextHistory").GetComponent<Text>();
        textToday = transform.Find("TopSection/TextGroup/TextToday").GetComponent<Text>();
        textNotFinished = transform.Find("TopSection/TextGroup/TextNotFinished").GetComponent<Text>();
        btnFinished = transform.Find("MiddleSection/Scroll View/Viewport/Content/BtnFinished").GetComponent<Button>();
        btnMain = transform.Find("BottomSection/BtnMain").GetComponent<Button>();
        btnAdd = transform.Find("BtnAdd").GetComponent<Button>();
        btnSelf = transform.Find("BottomSection/BtnSelf").GetComponent<Button>();
        imgMain = transform.Find("BottomSection/BtnMain/Image").GetComponent<Image>();
        imgSelf = transform.Find("BottomSection/BtnSelf/Image").GetComponent<Image>();
        bottomSection = transform.Find("BottomSection").GetComponent<Image>();
        content = transform.Find("MiddleSection/Scroll View/Viewport/Content");
    }

    private void Start()
    {
        btnAdd.onClick.AddListener(()=>{ OpenNewItem(); });
        
        btnFinished.onClick.AddListener(()=>{ ShowOrHideFinishedItem(); });
        btnMain.onClick.AddListener(()=>{ ChangePanel(EnumType.UIPanel.PnlMain); });
        btnSelf.onClick.AddListener(()=>{ ChangePanel(EnumType.UIPanel.PnlSelf); });

        itemPrefab = ResManager.Instance.LoadRes<GameObject>("ui", "Item");
        id = PlayerPrefs.GetInt("ID", 0);
        ReadDataFromAllItem();
        OnModifyColorThemeEvent("");
    }

    private void OnEnable()
    {
        EventHandler.AddNewItemEvent += OnAddNewItemEvent;
        EventHandler.ModifyItemEvent += OnModifyItemEvent;
        EventHandler.ModifyColorThemeEvent += OnModifyColorThemeEvent;
    }

    private void OnDisable()
    {
        EventHandler.AddNewItemEvent -= OnAddNewItemEvent;
        EventHandler.ModifyItemEvent -= OnModifyItemEvent;
        EventHandler.ModifyColorThemeEvent -= OnModifyColorThemeEvent;
    }

    private void OpenNewItem()
    {
        UIManager.Instance.CreatePanel(EnumType.UIPanel.PnlNewItem, UIManager.Instance.TopCanvas);
    }

    private void ChangePanel(EnumType.UIPanel panel)
    {
        currentPanel = panel;
        GameObject selfPanel = UIManager.Instance.GetPanel(EnumType.UIPanel.PnlSelf);
        
        switch(panel)
        {
            case EnumType.UIPanel.PnlMain:
                UIManager.Instance.SetImageWithColor(imgMain, "icon_home_active");
                UIManager.Instance.SetImageWithColor(imgSelf, "icon_self_inactive");
                selfPanel?.SetActive(false);
                break;
            case EnumType.UIPanel.PnlSelf:
                UIManager.Instance.SetImageWithColor(imgMain, "icon_home_inactive");
                UIManager.Instance.SetImageWithColor(imgSelf, "icon_self_active");
                if(selfPanel == null)
                {
                    UIManager.Instance.CreatePanel(EnumType.UIPanel.PnlSelf, UIManager.Instance.MainCanvas);
                }
                else
                {
                    selfPanel.SetActive(true);
                }
                break;
        }
    }

    private void OnAddNewItemEvent(string text)
    {
        id++;
        PlayerPrefs.SetInt("ID", id);
        string now = DateTime.Now.ToString();

        CreateItem(id, text, now, false);

        // 添加数据
        DataManager.Instance.AddItemData(id, text, false, now, now, "");

        int historyCount = DataManager.Instance.allItem.items.Count;
        textHistory.text = $"历史 {historyCount} 件事";

        int notfinishedCount = DataManager.Instance.CountNotFinished();
        textNotFinished.text = $"待完成 {notfinishedCount} 件事";
    }

    private void OnModifyItemEvent(string text)
    {
        Text itemText = selectedItem.transform.Find("TextButton/Text").GetComponent<Text>();
        itemText.text = text;
        Text idText = selectedItem.transform.Find("IDText").GetComponent<Text>();
        Toggle toggleButton = selectedItem.transform.Find("Toggle").GetComponent<Toggle>();
        DataManager.Instance.ModifyItemData(int.Parse(idText.text), text, toggleButton.isOn,
            DateTime.Now.ToString(), DateTime.Now.ToString());
    }

    private void OnModifyColorThemeEvent(string colorTheme)
    {
        Button[] buttons = new Button[]{ btnFinished };
        ThemeManager.Instance.ChangeButtonStyle(buttons);
        ThemeManager.Instance.ChangeBtnAddStyle(btnAdd);
        List<Image> imageList = new List<Image>();
        List<Toggle> toggleList = new List<Toggle>();
        List<Button> buttonList = new List<Button>();
        for(int i = 0; i < content.transform.childCount; i++)
        {
            Image img = content.transform.GetChild(i).GetComponent<Image>();
            imageList.Add(img);
            Toggle toggle = content.transform.GetChild(i).Find("Toggle")?.GetComponent<Toggle>();
            if(toggle)
            {
                toggleList.Add(toggle);
            }
            Button button = content.transform.GetChild(i).Find("DeleteButton")?.GetComponent<Button>();
            if(button)
            {
                buttonList.Add(button);
            }
        }
        imageList.Add(bottomSection);
        ThemeManager.Instance.ChangeImageStyle(imageList.ToArray());
        ThemeManager.Instance.ChangeButtonStyle(buttonList.ToArray());
        ThemeManager.Instance.ChangeToggleStyle(toggleList.ToArray());
        ChangePanel(currentPanel);
    }

    private void CreateItem(int id, string text, string date, bool isFinished)
    {
        // 实例化事项，并添加到滚动视图中
        GameObject item = Instantiate(itemPrefab, content.transform);
        item.name = "Item";
        item.transform.SetAsFirstSibling();

        // 修改事项 ID
        Text idText = item.transform.Find("IDText").GetComponent<Text>();
        idText.text = id.ToString();

        // 修改事项文本
        Text itemText = item.transform.Find("TextButton/Text").GetComponent<Text>();
        itemText.text = text;

        // 修改事项时间文本
        Text itemDateText = item.transform.Find("TextButton/DateText").GetComponent<Text>();
        itemDateText.text = date;

        // 修改事项文本按钮
        Button textButton = item.transform.Find("TextButton").GetComponent<Button>();
        textButton.onClick.AddListener(delegate
        {
            selectedItem = item;
            UIManager.Instance.CreatePanel(EnumType.UIPanel.PnlModifyItem, UIManager.Instance.TopCanvas);
            GameObject obj = UIManager.Instance.GetPanel(EnumType.UIPanel.PnlModifyItem);
            obj.transform.GetComponent<PnlModifyItem>().UpdateInput(itemText.text);
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
            int notfinishedCount = DataManager.Instance.CountNotFinished();
            textNotFinished.text = $"待完成 {notfinishedCount} 件事";
        });

        toggleButton.isOn = isFinished;

        string str = date.Substring(0, date.Length - 9);
        string now = DateTime.Now.ToString();

        // 修改删除按钮
        Button deleteButton = item.transform.Find("DeleteButton").GetComponent<Button>();
        deleteButton.onClick.AddListener(delegate
        {
            DataManager.Instance.DeleteItemData(int.Parse(idText.text), itemText.text, false);

            int historyCount = DataManager.Instance.allItem.items.Count;
            textHistory.text = $"历史 {historyCount} 件事";

            int notfinishedCount = DataManager.Instance.CountNotFinished();
            textNotFinished.text = $"待完成 {notfinishedCount} 件事";

            if (now.Contains(str))
            {
                count--;
                textToday.text = $"今天 {count} 件事";
            }
            
            Destroy(item);
        });

        if (now.Contains(str))
        {
            // 修改事项数量
            count++;
            textToday.text = $"今天 {count} 件事";
        }
    }

    /// <summary>
    /// 完成事项
    /// </summary>
    void FinishItem(GameObject item)
    {
        item.transform.SetSiblingIndex(btnFinished.transform.GetSiblingIndex());
        item.SetActive(isItemShow);
        item.GetComponent<CanvasGroup>().alpha = 0.5f;
    }

    /// <summary>
    /// 恢复事项
    /// </summary>
    void RecoverItem(GameObject item)
    {
        item.transform.SetSiblingIndex(btnFinished.transform.GetSiblingIndex());
        item.GetComponent<CanvasGroup>().alpha = 1;
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
    /// 从 allItem 里读取事项
    /// </summary>
    void ReadDataFromAllItem()
    {
        foreach(Item child in DataManager.Instance.allItem.items)
        {
            CreateItem(child.itemID, child.itemContent, child.itemCreatedDate, child.isFinished);
        }

        int historyCount = DataManager.Instance.allItem.items.Count;
        textHistory.text = $"历史 {historyCount} 件事";
        int notfinishedCount = DataManager.Instance.CountNotFinished();
        textNotFinished.text = $"待完成 {notfinishedCount} 件事";
    }
}
