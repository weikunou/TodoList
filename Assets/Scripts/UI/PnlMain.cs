using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PnlMain : MonoBehaviour
{
    Image imgMain, imgShop, imgSelf, bottomSection;
    Button btnMain, btnShop, btnSelf;
    Transform topSection;
    List<GameObject> panels = new List<GameObject>();
    Dictionary<EnumType.UIPanel, Image> imgDic = new Dictionary<EnumType.UIPanel, Image>();
    EnumType.UIPanel currentPanel = EnumType.UIPanel.PnlHome;

    private void Awake()
    {
        imgMain = transform.Find("BottomSection/BtnMain/Image").GetComponent<Image>();
        imgShop = transform.Find("BottomSection/BtnShop/Image").GetComponent<Image>();
        imgSelf = transform.Find("BottomSection/BtnSelf/Image").GetComponent<Image>();
        topSection = transform.Find("TopSection");
        bottomSection = transform.Find("BottomSection").GetComponent<Image>();
        btnMain = transform.Find("BottomSection/BtnMain").GetComponent<Button>();
        btnShop = transform.Find("BottomSection/BtnShop").GetComponent<Button>();
        btnSelf = transform.Find("BottomSection/BtnSelf").GetComponent<Button>();

        btnMain.onClick.AddListener(()=>{ ChangePanel(EnumType.UIPanel.PnlHome); });
        btnShop.onClick.AddListener(()=>{ ChangePanel(EnumType.UIPanel.PnlShop); });
        btnSelf.onClick.AddListener(()=>{ ChangePanel(EnumType.UIPanel.PnlSelf); });

        InitPanel();
    }

    private void Start()
    {
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

    private void InitPanel()
    {
        UIManager.Instance.CreatePanel(EnumType.UIPanel.PnlHome, topSection);
        UIManager.Instance.CreatePanel(EnumType.UIPanel.PnlShop, topSection);
        UIManager.Instance.CreatePanel(EnumType.UIPanel.PnlSelf, topSection);
        
        GameObject homePanel = UIManager.Instance.GetPanel(EnumType.UIPanel.PnlHome);
        GameObject shopPanel = UIManager.Instance.GetPanel(EnumType.UIPanel.PnlShop);
        GameObject selfPanel = UIManager.Instance.GetPanel(EnumType.UIPanel.PnlSelf);
        
        panels.Add(homePanel);
        panels.Add(shopPanel);
        panels.Add(selfPanel);

        imgDic.Add(EnumType.UIPanel.PnlHome, imgMain);
        imgDic.Add(EnumType.UIPanel.PnlShop, imgShop);
        imgDic.Add(EnumType.UIPanel.PnlSelf, imgSelf);
    }

    private void ChangePanel(EnumType.UIPanel panel)
    {
        // 切换页面显示
        CanvasGroup canvasGroup;
        for(int i = 0; i < panels.Count; i++)
        {
            canvasGroup = panels[i].transform.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
        canvasGroup = panels[(int)panel - 1].transform.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        // 切换图标
        UIManager.Instance.SetImageWithColor(imgMain, "icon_home_inactive");
        UIManager.Instance.SetImageWithColor(imgShop, "icon_shop_inactive");
        UIManager.Instance.SetImageWithColor(imgSelf, "icon_self_inactive");

        UIManager.Instance.SetImageWithColor(imgDic[panel], string.Format("icon_{0}_active", panel.ToString().Substring(3).ToLower()));
    }
}
