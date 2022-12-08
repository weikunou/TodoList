using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PnlHome : MonoBehaviour
{
    Image topSection;
    Text textHistory, textToday, textNotFinished;
    Button btnAdd, btnSettings, btnNotFinished, btnFinished;
    LoopScroll loopScroll;

    Transform content;
    GameObject itemPrefab;
    GameObject selectedItem;

    int id;
    bool finishedTag = false;

    private void Awake()
    {
        topSection = transform.Find("TopSection").GetComponent<Image>();
        textHistory = transform.Find("TopSection/TextGroup/TextHistory").GetComponent<Text>();
        textToday = transform.Find("TopSection/TextGroup/TextToday").GetComponent<Text>();
        textNotFinished = transform.Find("TopSection/TextGroup/TextNotFinished").GetComponent<Text>();
        
        btnNotFinished = transform.Find("TopSection/BtnGroup/BtnNotFinished").GetComponent<Button>();
        btnFinished = transform.Find("TopSection/BtnGroup/BtnFinished").GetComponent<Button>();
        
        btnAdd = transform.Find("BtnAdd").GetComponent<Button>();
        
        loopScroll = transform.Find("MiddleSection/Scroll View").GetComponent<LoopScroll>();
        content = transform.Find("MiddleSection/Scroll View/Viewport/Content");

        btnNotFinished.onClick.AddListener(()=>{ finishedTag = false; RefreshNotFinished(); });
        btnFinished.onClick.AddListener(()=>{ finishedTag = true; RefreshFinished(); });
    }

    private void Start()
    {
        btnAdd.onClick.AddListener(()=>{ OpenNewItem(); });
        itemPrefab = ResManager.Instance.LoadRes<GameObject>("ui", "Item");
        id = PlayerPrefs.GetInt("ID", 0);
        OnUpdateHomeDataEvent(0);
        OnModifyColorThemeEvent("");
    }

    private void OnEnable()
    {
        EventHandler.AddNewItemEvent += OnAddNewItemEvent;
        EventHandler.ModifyItemEvent += OnModifyItemEvent;
        EventHandler.ModifyColorThemeEvent += OnModifyColorThemeEvent;
        EventHandler.UpdateHomeDataEvent += OnUpdateHomeDataEvent;
    }

    private void OnDisable()
    {
        EventHandler.AddNewItemEvent -= OnAddNewItemEvent;
        EventHandler.ModifyItemEvent -= OnModifyItemEvent;
        EventHandler.ModifyColorThemeEvent -= OnModifyColorThemeEvent;
        EventHandler.UpdateHomeDataEvent -= OnUpdateHomeDataEvent;
    }

    private void OpenNewItem()
    {
        UIManager.Instance.CreatePanel(EnumType.UIPanel.PnlNewItem, UIManager.Instance.TopCanvas);
    }

    private void OnAddNewItemEvent(string text)
    {
        id++;
        PlayerPrefs.SetInt("ID", id);
        string now = DateTime.Now.ToString();
        // 添加数据
        DataManager.Instance.AddItemData(id, text, false, now, now, "");
        OnUpdateHomeDataEvent(0);
    }

    private void OnModifyItemEvent(int id, string text)
    {
        DataManager.Instance.ModifyItemDataOnlyContent(id, text);
        if(finishedTag)
        {
            RefreshFinished();
        }
        else
        {
            RefreshNotFinished();
        }
    }

    private void OnModifyColorThemeEvent(string colorTheme)
    {
        Button[] buttons = new Button[]{ btnNotFinished, btnFinished };
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
        imageList.Add(topSection);
        ThemeManager.Instance.ChangeImageStyle(imageList.ToArray());
        ThemeManager.Instance.ChangeButtonStyle(buttonList.ToArray());
        ThemeManager.Instance.ChangeToggleStyle(toggleList.ToArray());
    }

    private void OnUpdateHomeDataEvent(int count)
    {
        int historyCount = DataManager.Instance.allItem.items.Count;
        textHistory.text = $"历史 {historyCount} 件事";

        int notfinishedCount = DataManager.Instance.CountNotFinished();
        textNotFinished.text = $"待完成 {notfinishedCount} 件事";

        int todayCount = DataManager.Instance.CountToday();
        textToday.text = $"今天 {todayCount} 件事";

        if(finishedTag)
        {
            RefreshFinished();
        }
        else
        {
            RefreshNotFinished();
        }
    }

    private void RefreshNotFinished()
    {
        loopScroll.Refresh(DataManager.Instance.notFinishedItems);
    }

    private void RefreshFinished()
    {
        loopScroll.Refresh(DataManager.Instance.finishedItems);
    }
}
