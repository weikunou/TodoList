using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlModifyInfo : MonoBehaviour
{
    Button btnCancel, btnConfirm;
    InputField inputName;

    private void Awake()
    {
        btnCancel = transform.Find("Popup/BtnCancel").GetComponent<Button>();
        btnConfirm = transform.Find("Popup/BtnConfirm").GetComponent<Button>();
        inputName = transform.Find("Popup/InputField").GetComponent<InputField>();
    }

    private void Start()
    {
        btnCancel.onClick.AddListener(()=>{ ClosePanel(); });
        btnConfirm.onClick.AddListener(()=>{ ModifyInfo(); });

        inputName.text = PlayerPrefs.GetString("Name", "用户101");
    }

    private void ModifyInfo()
    {
        if (inputName.text.Equals(""))
        {
            Debug.LogWarning("名字不能为空");
            return;
        }
        PlayerPrefs.SetString("Name", inputName.text);
        EventHandler.CallModifyInfoEvent(inputName.text);
        ClosePanel();
    }

    private void ClosePanel()
    {
        UIManager.Instance.DestroyPanel(EnumType.UIPanel.PnlModifyInfo);
    }
}
