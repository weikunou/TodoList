﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// 数据管理器类
/// </summary>
public class DataManager : Singleton<DataManager>
{
    /// <summary>
    /// 所有的事项
    /// </summary>
    public AllItem allItem = new AllItem();

    public List<Item> notFinishedItems = new List<Item>();
    public List<Item> finishedItems = new List<Item>();

    /// <summary>
    /// 保存的 json
    /// </summary>
    string json;

    string path;

    void Start()
    {
        path = Application.persistentDataPath + "/Data.json";

        if (PlayerPrefs.GetString("isFirstStart", "Yes").Equals("Yes"))
        {
            json = JsonUtility.ToJson(allItem);
            WriteJson();
            PlayerPrefs.SetString("isFirstStart", "No");
        }

        ReadJson();
        TransformToAllItem();
    }

    /// <summary>
    /// 添加事项数据
    /// </summary>
    /// <param name="itemID">事项 ID</param>
    /// <param name="itemContent">事项 内容</param>
    /// <param name="itemStatus">事项 状态</param>
    /// <param name="itemCreatedDate">事项 创建时间</param>
    /// <param name="itemModifiedDate">事项 修改时间</param>
    /// <param name="itemFinishedDate">事项 完成时间</param>
    public void AddItemData(int itemID, string itemContent, bool itemStatus,
        string itemCreatedDate, string itemModifiedDate, string itemFinishedDate)
    {
        Item itemData = new Item();
        itemData.itemID = itemID;
        itemData.itemContent = itemContent;
        itemData.isFinished = itemStatus;
        itemData.itemCreatedDate = itemCreatedDate;
        itemData.itemModifiedDate = itemModifiedDate;
        itemData.itemFinishedDate = itemFinishedDate;

        allItem.items.Add(itemData);
        SplitItemList();

        json = JsonUtility.ToJson(allItem);
        WriteJson();
    }

    /// <summary>
    /// 修改事项数据
    /// </summary>
    /// <param name="itemID">事项 ID</param>
    /// <param name="itemContent">事项 内容</param>
    /// <param name="itemStatus">事项 状态</param>
    /// <param name="itemModifiedDate">事项 修改时间</param>
    /// <param name="itemFinishedDate">事项 完成时间</param>
    public void ModifyItemData(int itemID, string itemContent, bool itemStatus, string itemModifiedDate, string itemFinishedDate)
    {
        foreach(Item child in allItem.items)
        {
            // 找到相同 ID 的事项数据
            if(child.itemID == itemID)
            {
                child.itemContent = itemContent;
                child.isFinished = itemStatus;
                child.itemModifiedDate = itemModifiedDate;

                // 完成事项时修改完成时间
                if (itemStatus)
                {
                    child.itemFinishedDate = itemFinishedDate;
                }
            }
        }

        json = JsonUtility.ToJson(allItem);
        WriteJson();
    }

    public void ModifyItemDataOnlyContent(int id, string content)
    {
        for(int i = 0; i < allItem.items.Count; i++)
        {
            if(allItem.items[i].itemID == id)
            {
                allItem.items[i].itemContent = content;
            }
        }
        json = JsonUtility.ToJson(allItem);
        WriteJson();
    }

    public void ModifyItemDataOnlyFinished(int id, bool isFinished)
    {
        for(int i = 0; i < allItem.items.Count; i++)
        {
            if(allItem.items[i].itemID == id)
            {
                allItem.items[i].isFinished = isFinished;
            }
        }
        SplitItemList();
        json = JsonUtility.ToJson(allItem);
        WriteJson();
    }

    public void DeleteItemData(int itemID)
    {
        for(int i = allItem.items.Count - 1; i >= 0; i--)
        {
            // 找到相同 ID 的事项数据
            if (allItem.items[i].itemID == itemID)
            {
                allItem.items.Remove(allItem.items[i]);
            }
        }
        SplitItemList();
        json = JsonUtility.ToJson(allItem);
        WriteJson();
    }

    public int CountNotFinished()
    {
        int notFinishedCount = 0;
        for(int i = 0; i < allItem.items.Count; i++)
        {
            if (!allItem.items[i].isFinished)
            {
                notFinishedCount++;
            }
        }
        return notFinishedCount;
    }

    public int CountToday()
    {
        int todayCount = 0;
        string now = DateTime.Now.ToString();
        for(int i = 0; i < allItem.items.Count; i++)
        {
            string date = allItem.items[i].itemCreatedDate;
            string str = date.Substring(0, date.Length - 9);
            if (now.Contains(str))
            {
                todayCount++;
            }
        }
        return todayCount;
    }

    /// <summary>
    /// 读取 json
    /// </summary>
    public void ReadJson()
    {
        StreamReader sr = new StreamReader(path);

        json = sr.ReadLine();

        sr.Close();
    }

    /// <summary>
    /// 写入 json
    /// </summary>
    public void WriteJson()
    {
        StreamWriter sw = new StreamWriter(path, false);

        sw.Write(json);

        sw.Flush();

        sw.Close();
    }

    public void TransformToAllItem()
    {
        allItem = JsonUtility.FromJson<AllItem>(json);
        SplitItemList();
    }

    public void SplitItemList()
    {
        notFinishedItems.Clear();
        finishedItems.Clear();
        int sum = allItem.items.Count - 1;
        for(int i = sum; i >= 0; i--)
        {
            if(allItem.items[i].isFinished)
            {
                finishedItems.Add(allItem.items[i]);
            }
            else
            {
                notFinishedItems.Add(allItem.items[i]);
            }
        }
    }
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
    /// <summary>
    /// 事项 ID
    /// </summary>
    public int itemID;

    /// <summary>
    /// 事项内容
    /// </summary>
    public string itemContent = "";

    /// <summary>
    /// 事项完成状态
    /// </summary>
    public bool isFinished = false;

    /// <summary>
    /// 事项创建时间
    /// </summary>
    public string itemCreatedDate = "";

    /// <summary>
    /// 事项修改时间
    /// </summary>
    public string itemModifiedDate = "";

    /// <summary>
    /// 事项完成时间
    /// </summary>
    public string itemFinishedDate = "";
}