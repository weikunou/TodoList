using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlMain : MonoBehaviour
{
    Image imgMain, imgSelf, bottomSection;
    Button btnMain, btnSelf;
    Transform topSection;
    EnumType.UIPanel currentPanel = EnumType.UIPanel.PnlHome;

    private void Awake()
    {
        imgMain = transform.Find("BottomSection/BtnMain/Image").GetComponent<Image>();
        imgSelf = transform.Find("BottomSection/BtnSelf/Image").GetComponent<Image>();
        topSection = transform.Find("TopSection");
        bottomSection = transform.Find("BottomSection").GetComponent<Image>();
        btnMain = transform.Find("BottomSection/BtnMain").GetComponent<Button>();
        btnSelf = transform.Find("BottomSection/BtnSelf").GetComponent<Button>();
    }

    private void Start()
    {
        btnMain.onClick.AddListener(()=>{ ChangePanel(EnumType.UIPanel.PnlHome); });
        btnSelf.onClick.AddListener(()=>{ ChangePanel(EnumType.UIPanel.PnlSelf); });
        ChangePanel(EnumType.UIPanel.PnlHome);
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
        Image[] images = new Image[]{ bottomSection };
        ThemeManager.Instance.ChangeImageStyle(images);
        ChangePanel(currentPanel);
    }

    private void ChangePanel(EnumType.UIPanel panel)
    {
        currentPanel = panel;
        GameObject homePanel = UIManager.Instance.GetPanel(EnumType.UIPanel.PnlHome);
        GameObject selfPanel = UIManager.Instance.GetPanel(EnumType.UIPanel.PnlSelf);
        
        switch(panel)
        {
            case EnumType.UIPanel.PnlHome:
                UIManager.Instance.SetImageWithColor(imgMain, "icon_home_active");
                UIManager.Instance.SetImageWithColor(imgSelf, "icon_self_inactive");
                if(homePanel == null)
                {
                    UIManager.Instance.CreatePanel(EnumType.UIPanel.PnlHome, topSection);
                }
                else
                {
                    CanvasGroup cg = homePanel.GetComponent<CanvasGroup>();
                    cg.alpha = 1;
                    cg.blocksRaycasts = true;
                }
                if(selfPanel != null)
                {
                    CanvasGroup cg = selfPanel.GetComponent<CanvasGroup>();
                    cg.alpha = 0;
                    cg.blocksRaycasts = false;
                }
                break;
            case EnumType.UIPanel.PnlSelf:
                UIManager.Instance.SetImageWithColor(imgMain, "icon_home_inactive");
                UIManager.Instance.SetImageWithColor(imgSelf, "icon_self_active");
                if(selfPanel == null)
                {
                    UIManager.Instance.CreatePanel(EnumType.UIPanel.PnlSelf, topSection);
                }
                else
                {
                    CanvasGroup cg = selfPanel.GetComponent<CanvasGroup>();
                    cg.alpha = 1;
                    cg.blocksRaycasts = true;
                }
                if(homePanel != null)
                {
                    CanvasGroup cg = homePanel.GetComponent<CanvasGroup>();
                    cg.alpha = 0;
                    cg.blocksRaycasts = false;
                }
                break;
        }
    }
}
