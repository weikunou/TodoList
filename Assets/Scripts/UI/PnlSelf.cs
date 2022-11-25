using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlSelf : MonoBehaviour
{
    Button btnSettings;

    private void Awake()
    {
        btnSettings = transform.Find("BtnSettings").GetComponent<Button>();
    }

    private void Start()
    {
        btnSettings.onClick.AddListener(()=>{ OpenSettings(); });
    }

    private void OpenSettings()
    {
        UIManager.Instance.CreatePanel(EnumType.UIPanel.PnlSettings, UIManager.Instance.TopCanvas);
    }
}
