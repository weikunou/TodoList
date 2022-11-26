using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlModifyInfo : MonoBehaviour
{
    Button btnCancel, btnConfirm;
    InputField inputName, inputIntro;

    private void Awake()
    {
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
