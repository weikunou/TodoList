using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PnlModifyItem : MonoBehaviour
{
    Image popup;
    InputField inputField;
    Button btnCancel, btnConfirm;

    int id;

    private void Awake()
    {
        popup = transform.Find("Popup").GetComponent<Image>();
        inputField = transform.Find("Popup/InputField").GetComponent<InputField>();
        btnCancel = transform.Find("Popup/BtnCancel").GetComponent<Button>();
        btnConfirm = transform.Find("Popup/BtnConfirm").GetComponent<Button>();
    }

    private void Start()
    {
        btnCancel.onClick.AddListener(()=>{ ClosePanel(); });
        btnConfirm.onClick.AddListener(()=>{ ModifyItem(); });

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
    }

    public void UpdateInput(int id, string text)
    {
        inputField.text = text;
        this.id = id;
    }

    private void ModifyItem()
    {
        // 检查输入框
        if (inputField.text.Equals(""))
        {
            Debug.LogWarning("请输入待办事项内容！");
            return;
        }

        EventHandler.CallModifyItemEvent(this.id, inputField.text);
        EventHandler.CallModifyColorThemeEvent("");

        // 关闭弹窗
        ClosePanel();
    }

    private void ClosePanel()
    {
        UIManager.Instance.DestroyPanel(EnumType.UIPanel.PnlModifyItem);
    }
}
