using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// 游戏管理器类
/// </summary>
public class GameManager : Singleton<GameManager>
{

    /// <summary>
    /// 设置窗口
    /// </summary>
    public GameObject settingPanel;

    /// <summary>
    /// 事项预制体
    /// </summary>
    [Header("预制体资源")]
    public GameObject itemPrefab;

    /// <summary>
    /// 事项未完成的颜色
    /// </summary>
    public Color itemColor;

    /// <summary>
    /// 事项完成后的颜色
    /// </summary>
    public Color itemFinishedColor;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
