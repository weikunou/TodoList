using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlSelf : MonoBehaviour
{
    Button btnModifyInfo, btnSettings;
    Text textName;

    private void Awake()
    {
        btnModifyInfo = transform.Find("BtnModifyInfo").GetComponent<Button>();
        btnSettings = transform.Find("BtnSettings").GetComponent<Button>();
        textName = transform.Find("TextName").GetComponent<Text>();
    }

    private void Start()
    {
        btnModifyInfo.onClick.AddListener(()=>{ OpenModifyInfo(); });
        btnSettings.onClick.AddListener(()=>{ OpenSettings(); });
    }

    private void OnEnable()
    {
        EventHandler.ModifyInfoEvent += OnModifyInfoEvent;
    }

    private void OnDisable()
    {
        EventHandler.ModifyInfoEvent -= OnModifyInfoEvent;
    }

    private void OnModifyInfoEvent(string text)
    {
        textName.text = text;
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
