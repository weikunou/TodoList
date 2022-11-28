using System;

public static class EventHandler
{
    public static event Action<string> AddNewItemEvent;

    public static void CallAddNewItemEvent(string text)
    {
        AddNewItemEvent?.Invoke(text);
    }

    public static event Action<string> ModifyItemEvent;

    public static void CallModifyItemEvent(string text)
    {
        ModifyItemEvent?.Invoke(text);
    }

    public static event Action<string, string> ModifyInfoEvent;

    public static void CallModifyInfoEvent(string text_name, string text_intro)
    {
        ModifyInfoEvent?.Invoke(text_name, text_intro);
    }

    public static event Action<string> ModifyColorThemeEvent;

    public static void CallModifyColorThemeEvent(string colorTheme)
    {
        ModifyColorThemeEvent?.Invoke(colorTheme);
    }
}
