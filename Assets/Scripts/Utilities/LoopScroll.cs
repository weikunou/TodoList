using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopScroll : MonoBehaviour
{
    /// <summary>
    /// 数据项
    /// </summary>
    public RectTransform item;

    /// <summary>
    /// 滚动视图
    /// </summary>
    ScrollRect scrollRect;

    /// <summary>
    /// 预制体列表
    /// </summary>
    /// <typeparam name="RectTransform">预制体的坐标信息</typeparam>
    /// <returns></returns>
    List<RectTransform> itemList = new List<RectTransform>();

    /// <summary>
    /// 数据列表
    /// </summary>
    /// <typeparam name="string">数据类型</typeparam>
    /// <returns></returns>
    List<Item> dataList = new List<Item>();

    void Awake()
    {
        // 获取组件
        scrollRect = GetComponent<ScrollRect>();

        // 添加监听滚动事件
        scrollRect.onValueChanged.AddListener(OnScrollChanged);

        Init();
    }

    /// <summary>
    /// 额外的item数量
    /// </summary>
    public int extra;

    public float space;
    public float top;

    public void Init()
    {
        float viewport_height = scrollRect.viewport.rect.height;
        int num = Mathf.CeilToInt(viewport_height / item.rect.height);
        int sum = num + extra;
        tailItem = sum - 1;
        tailData = sum - 1;
        for(int  i = 0; i < sum; i++)
        {
            AddItem();
        }
    }

    public void ResetPoint()
    {
        float viewport_height = scrollRect.viewport.rect.height;
        int num = Mathf.CeilToInt(viewport_height / item.rect.height);
        int sum = num + extra;
        headItem = 0;
        headData = 0;
        tailItem = sum - 1;
        tailData = sum - 1;
    }

    /// <summary>
    /// 头节点索引
    /// </summary>
    public int headItem;
    
    /// <summary>
    /// 尾节点索引
    /// </summary>
    public int tailItem;

    /// <summary>
    /// 头数据索引
    /// </summary>
    public int headData;

    /// <summary>
    /// 尾数据索引
    /// </summary>
    public int tailData;

    /// <summary>
    /// 刷新视图
    /// </summary>
    public void Refresh(List<Item> data)
    {
        dataList = data;
        ResetPoint();
        CountItemPos();
    }

    /// <summary>
    /// 添加单条内容
    /// </summary>
    public void AddItem()
    {
        GameObject obj = Instantiate(item.gameObject, scrollRect.content.transform);
        obj.SetActive(true);
        itemList.Add(obj.GetComponent<RectTransform>());
    }

    /// <summary>
    /// 计算每条内容的位置和content的总高度
    /// </summary>
    public void CountItemPos()
    {
        // 计算 item 位置
        float pos = top;

        int currentItem = headItem; // 从头节点的位置开始
        for(int i = headData; i <= tailData; i++)
        {
            // 如果提前到达末尾，则返回头部继续
            if(currentItem >= itemList.Count)
            {
                currentItem = 0;
            }
            itemList[currentItem].anchoredPosition = new Vector2(0, pos);
            if(i < dataList.Count)
            {
                itemList[currentItem].GetComponent<LoopItem>().UpdateSelf(dataList[i]);
                itemList[currentItem].gameObject.SetActive(true);
                // 这里的 pos 算出来刚好是下一个 Item 出现的 PosY 位置，正好是 Content 的高度
                pos -= (itemList[currentItem].rect.height + space);
            }
            else
            {
                itemList[currentItem].gameObject.SetActive(false);
            }
            currentItem++;
        }
        // 计算 content 高度
        float full_heihgt = Mathf.Abs(pos);
        scrollRect.content.sizeDelta = new Vector2(0, full_heihgt);
    }

    /// <summary>
    /// 滚动回调
    /// </summary>
    /// <param name="normalisedPos">归一化位置</param>
    public void OnScrollChanged(Vector2 normalisedPos)
    {
        OnVerticalChanged();
    }

    /// <summary>
    /// 垂直滚动
    /// </summary>
    public void OnVerticalChanged()
    {
        // content 的 y 坐标
        float content_y = scrollRect.content.anchoredPosition.y;

        // 获取头节点索引，并作越界处理
        int currentItem = headItem;
        if(currentItem + 1 >= itemList.Count)
        {
            currentItem = -1;
        }

        // 第二个 item 的 y 坐标
        float next_item_y = itemList[currentItem + 1].anchoredPosition.y;

        // 第一个 item 的 y 坐标
        float head_item_y = itemList[headItem].anchoredPosition.y;

        // 当第一个 Item 移动到视图外，也就是第二个 Item 移动到顶部了
        if(content_y > Mathf.Abs(next_item_y))
        {
            // 防止数据列表越界
            if(tailData >= dataList.Count - 1)
            {
                return;
            }

            // 数据列表区间往后移动一个单位
            headData++;
            tailData++;

            // 先获取尾节点的 y 坐标位置
            float pos_y = itemList[tailItem].anchoredPosition.y;

            // 调整一下代码的执行顺序，要在计算 pos_y 之前，先更新 Item 高度
            itemList[headItem].GetComponent<LoopItem>().UpdateSelf(dataList[tailData]);

            // 在尾节点的位置上，往下移动尾节点的高度和间隔
            pos_y -= (itemList[tailItem].rect.height + space);

            // 将新的位置赋值给头节点，并使用尾数据
            itemList[headItem].anchoredPosition = new Vector2(0, pos_y);

            // 尾节点索引移动到头节点索引，头节点索引往后移动一个单位，并作越界处理
            tailItem = headItem;
            headItem++;
            if(headItem >= itemList.Count)
            {
                headItem = 0;
            }

            // content 高度增加一个尾节点高度和间隔
            float full_heihgt = scrollRect.content.rect.height + (itemList[tailItem].rect.height + space);
            scrollRect.content.sizeDelta = new Vector2(0, full_heihgt);
        }
        // 当第一个 Item 移动到视图内
        else if(content_y < Mathf.Abs(head_item_y))
        {
            // 防止数据列表越界
            if(headData <= 0)
            {
                return;
            }

            // 数据列表区间往前移动一个单位
            headData--;
            tailData--;

            // 先获取头节点的 y 坐标位置
            float pos_y = itemList[headItem].anchoredPosition.y;

            // 调整一下代码的执行顺序，要在 Item 高度更新之前，先减去原来的高度
            // content 高度减少一个尾节点高度和间隔
            float full_heihgt = scrollRect.content.rect.height - (itemList[tailItem].rect.height + space);
            scrollRect.content.sizeDelta = new Vector2(0, full_heihgt);

            // 调整一下代码的执行顺序，要在计算 pos_y 之前，先更新 Item 高度
            itemList[tailItem].GetComponent<LoopItem>().UpdateSelf(dataList[headData]);

            // 在头节点的位置上，往上移动尾节点的高度和间隔
            pos_y += (itemList[tailItem].rect.height + space);

            // 将新的位置赋值给尾节点，并使用头数据
            itemList[tailItem].anchoredPosition = new Vector2(0, pos_y);

            // 头节点索引移动到尾节点索引，尾节点索引往前移动一个单位，并作越界处理
            headItem = tailItem;
            tailItem--;
            if(tailItem < 0)
            {
                tailItem = itemList.Count - 1;
            }
        }
    }
}
