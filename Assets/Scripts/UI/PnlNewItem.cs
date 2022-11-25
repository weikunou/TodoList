using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PnlNewItem : MonoBehaviour
{
    InputField inputField;
    Button btnCancel, btnConfirm;

    private void Awake()
    {
        inputField = transform.Find("Popup/InputField").GetComponent<InputField>();
        btnCancel = transform.Find("Popup/BtnCancel").GetComponent<Button>();
        btnConfirm = transform.Find("Popup/BtnConfirm").GetComponent<Button>();
    }

    private void Start()
    {
        btnCancel.onClick.AddListener(()=>{ ClosePanel(); });
        btnConfirm.onClick.AddListener(()=>{ AddNewItem(); });
    }

    private void AddNewItem()
    {
        // 检查输入框
        if (inputField.text.Equals(""))
        {
            Debug.LogWarning("请输入待办事项内容！");
            return;
        }

        // 触发添加待办事项事件
        EventHandler.CallAddNewItemEvent(inputField.text);

        // 关闭弹窗
        ClosePanel();
    }

    private void ClosePanel()
    {
        UIManager.Instance.DestroyPanel(EnumType.UIPanel.PnlNewItem);
    }
}
