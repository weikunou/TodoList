using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlAbout : MonoBehaviour
{
    Image popup;
    Button btnClsoe;

    private void Awake()
    {
        popup = transform.Find("Popup").GetComponent<Image>();
        btnClsoe = transform.Find("Popup/BtnClose").GetComponent<Button>();
    }

    private void Start()
    {
        btnClsoe.onClick.AddListener(()=>{ ClosePanel(); });
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
        Button[] buttons = new Button[]{ btnClsoe };
        ThemeManager.Instance.ChangeButtonStyle(buttons);
    }

    private void ClosePanel()
    {
        UIManager.Instance.DestroyPanel(EnumType.UIPanel.PnlAbout);
    }
}
