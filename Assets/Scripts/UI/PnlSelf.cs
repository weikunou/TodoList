using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlSelf : MonoBehaviour
{
    Button btnModifyInfo, btnSettings;
    Text textName, textIntro;

    private void Awake()
    {
        btnModifyInfo = transform.Find("BtnModifyInfo").GetComponent<Button>();
        btnSettings = transform.Find("BtnSettings").GetComponent<Button>();
        textName = transform.Find("Info/TextName").GetComponent<Text>();
        textIntro = transform.Find("Info/TextIntro").GetComponent<Text>();
    }

    private void Start()
    {
        btnModifyInfo.onClick.AddListener(()=>{ OpenModifyInfo(); });
        btnSettings.onClick.AddListener(()=>{ OpenSettings(); });

        textName.text = PlayerPrefs.GetString("Name", "用户101");
        textIntro.text = PlayerPrefs.GetString("Intro", "这个人什么都没写");

        OnModifyColorThemeEvent("");
    }

    private void OnEnable()
    {
        EventHandler.ModifyInfoEvent += OnModifyInfoEvent;
        EventHandler.ModifyColorThemeEvent += OnModifyColorThemeEvent;
    }

    private void OnDisable()
    {
        EventHandler.ModifyInfoEvent -= OnModifyInfoEvent;
        EventHandler.ModifyColorThemeEvent -= OnModifyColorThemeEvent;
    }

    private void OnModifyInfoEvent(string text_name, string text_intro)
    {
        textName.text = text_name;
        textIntro.text = text_intro;
    }

    private void OnModifyColorThemeEvent(string colorTheme)
    {
        Button[] buttons = new Button[]{ btnModifyInfo, btnSettings };
        ThemeManager.Instance.ChangeButtonStyle(buttons);
    }

    private void OpenModifyInfo()
    {
        UIManager.Instance.CreatePanel(EnumType.UIPanel.PnlModifyInfo, UIManager.Instance.TopCanvas);
    }

    private void OpenSettings()
    {
        UIManager.Instance.CreatePanel(EnumType.UIPanel.PnlSettings, UIManager.Instance.TopCanvas);
    }
}
