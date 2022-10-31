using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlSettings : MonoBehaviour
{
    Button pnlSettings, btnClsoe;

    private void Awake()
    {
        pnlSettings = transform.GetComponent<Button>();
        btnClsoe = transform.Find("Popup/BtnClose").GetComponent<Button>();
    }

    private void Start()
    {
        pnlSettings.onClick.AddListener(()=>{ ClosePanel(); });
        btnClsoe.onClick.AddListener(()=>{ ClosePanel(); });
    }

    private void ClosePanel()
    {
        UIManager.Instance.DestroyPanel(EnumType.UIPanel.PnlSettings);
    }
}
