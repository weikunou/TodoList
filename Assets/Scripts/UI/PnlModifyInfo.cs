using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlModifyInfo : MonoBehaviour
{
    Image popup;
    Button btnCancel, btnConfirm;
    InputField inputName, inputIntro;

    private void Awake()
    {
        popup = transform.Find("Popup").GetComponent<Image>();
        btnCancel = transform.Find("Popup/BtnCancel").GetComponent<Button>();
        btnConfirm = transform.Find("Popup/BtnConfirm").GetComponent<Button>();
        inputName = transform.Find("Popup/InputName").GetComponent<InputField>();
        inputIntro = transform.Find("Popup/InputIntro").GetComponent<InputField>();
    }

    private void Start()
    {
        btnCancel.onClick.AddListener(()=>{ ClosePanel(); });
        btnConfirm.onClick.AddListener(()=>{ ModifyInfo(); });

        inputName.text = PlayerPrefs.GetString("Name", "用户101");
        inputIntro.text = PlayerPrefs.GetString("Intro", "这个人什么都没写");

        OnModifyColorThemeEvent("");
    }

    private void OnEnable()
    {
        EventHandler.ModifyColorThemeEvent += OnModifyColorThemeEvent;
    }

    private void OnDisable()
    {
        EventHandler.ModifyColorThemeEvent -= OnModifyColorThemeEvent;
    }

    private void OnModifyColorThemeEvent(string colorTheme)
    {
        Image[] popups = new Image[]{ popup };
        ThemeManager.Instance.ChangePopupStyle(popups);
        Button[] buttons = new Button[]{ btnCancel, btnConfirm };
        ThemeManager.Instance.ChangeButtonStyle(buttons);
        InputField[] inputs = new InputField[]{ inputName, inputIntro };
        ThemeManager.Instance.ChangeInputStyle(inputs);
    }

    private void ModifyInfo()
    {
        if (inputName.text.Equals(""))
        {
            Debug.LogWarning("名字不能为空");
            return;
        }
        PlayerPrefs.SetString("Name", inputName.text);
        PlayerPrefs.SetString("Intro", inputIntro.text);
        EventHandler.CallModifyInfoEvent(inputName.text, inputIntro.text);
        ClosePanel();
    }

    private void ClosePanel()
    {
        UIManager.Instance.DestroyPanel(EnumType.UIPanel.PnlModifyInfo);
    }
}
