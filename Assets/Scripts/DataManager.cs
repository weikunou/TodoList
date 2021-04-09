﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数据管理器类
/// </summary>
public class DataManager : MonoBehaviour
{
    /// <summary>
    /// 单例
    /// </summary>
    public static DataManager instance;

    /// <summary>
    /// 所有的事项
    /// </summary>
    public AllItem allItem = new AllItem();

    /// <summary>
    /// 保存的 json
    /// </summary>
    string json;

    #region 生命周期函数

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    #endregion

    #region 自定义函数

    /// <summary>
    /// 添加事项数据
    /// </summary>
    public void AddItemData(int itemID, string itemContent, bool itemStatus)
    {
        Item itemData = new Item();
        itemData.itemID = itemID;
        itemData.itemContent = itemContent;
        itemData.isFinished = itemStatus;

        allItem.items.Add(itemData);

        json = JsonUtility.ToJson(allItem);
        Debug.Log("添加 " + json);
    }

    /// <summary>
    /// 修改事项数据
    /// </summary>
    public void ModifyItemData(int itemID, string itemContent, bool itemStatus)
    {
        foreach(Item child in allItem.items)
        {
            // 找到相同 ID 的事项数据
            if(child.itemID == itemID)
            {
                child.itemContent = itemContent;
                child.isFinished = itemStatus;
            }
        }

        json = JsonUtility.ToJson(allItem);
        Debug.Log("修改 " + json);
    }

    public void DeleteItemData(int itemID, string itemContent, bool itemStatus)
    {
        for(int i = 0; i < allItem.items.Count; i++)
        {
            // 找到相同 ID 的事项数据
            if (allItem.items[i].itemID == itemID)
            {
                allItem.items.Remove(allItem.items[i]);
            }
        }

        json = JsonUtility.ToJson(allItem);
        Debug.Log("删除 " + json);
    }

    #endregion
}

/// <summary>
/// 所有事项
/// </summary>
public class AllItem
{
    public List<Item> items = new List<Item>();
}

/// <summary>
/// 事项
/// </summary>
[Serializable]
public class Item
{
    public int itemID;
    public string itemContent = "";
    public bool isFinished = false;
}