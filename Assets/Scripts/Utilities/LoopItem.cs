using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopItem : MonoBehaviour
{
    /// <summary>
    /// Item 最小高度
    /// </summary>
    public float min_height = 140;

    /// <summary>
    /// 内边距
    /// </summary>
    public float padding = 40;

    /// <summary>
    /// 容器
    /// </summary>
    RectTransform m_rect;

    Text textContent, textID, textDate;
    Button btnText, btnDelete;
    Toggle togFinished;
    CanvasGroup canvasGroup;
    public int id;

    void Awake()
    {
        m_rect = transform.GetComponent<RectTransform>();

        textID = transform.Find("IDText").GetComponent<Text>();
        textContent = transform.Find("TextButton/Text").GetComponent<Text>();
        textDate = transform.Find("TextButton/DateText").GetComponent<Text>();

        btnText = transform.Find("TextButton").GetComponent<Button>();
        btnText.onClick.AddListener(delegate
        {
            UIManager.Instance.CreatePanel(EnumType.UIPanel.PnlModifyItem, UIManager.Instance.TopCanvas);
            GameObject obj = UIManager.Instance.GetPanel(EnumType.UIPanel.PnlModifyItem);
            obj.transform.GetComponent<PnlModifyItem>().UpdateInput(id, textContent.text);
        });

        btnDelete = transform.Find("DeleteButton").GetComponent<Button>();
        btnDelete.onClick.AddListener(delegate
        {
            DataManager.Instance.DeleteItemData(id);
            EventHandler.CallUpdateHomeDataEvent(0);
        });

        togFinished = transform.Find("Toggle").GetComponent<Toggle>();
        togFinished.onValueChanged.AddListener(delegate
        {
            DataManager.Instance.ModifyItemDataOnlyFinished(id, togFinished.isOn);
            EventHandler.CallUpdateHomeDataEvent(0);
        });

        canvasGroup = transform.GetComponent<CanvasGroup>();
    }

    public void UpdateSelf(Item item)
    {
        id = item.itemID;
        textID.text = item.itemID.ToString();
        textContent.text = item.itemContent;
        textDate.text = item.itemCreatedDate;
        togFinished.isOn = item.isFinished;
        canvasGroup.alpha = item.isFinished ? 0.5f : 1;

        float current_height = textContent.preferredHeight + padding;
        if (current_height < min_height)
        {
            current_height = min_height;
        }
        m_rect.sizeDelta = new Vector2(-padding * 2, current_height);
    }
}
