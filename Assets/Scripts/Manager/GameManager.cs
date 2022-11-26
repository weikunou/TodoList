using UnityEngine;

/// <summary>
/// 游戏管理器类
/// </summary>
public class GameManager : Singleton<GameManager>
{
    void Start()
    {
        ChangeFrame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ChangeFrame()
    {
        string frameStr = PlayerPrefs.GetString("Frame", "Default");
        if(frameStr.Equals("Default"))
        {
            Application.targetFrameRate = -1;
        }
        else if(frameStr.Equals("Medium"))
        {
            Application.targetFrameRate = 30;
        }
        else if(frameStr.Equals("High"))
        {
            Application.targetFrameRate = 60;
        }
    }
}
