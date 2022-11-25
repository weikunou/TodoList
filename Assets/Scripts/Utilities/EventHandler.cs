using System;

public static class EventHandler
{
    public static event Action<string> AddNewItemEvent;

    public static void CallAddNewItemEvent(string text)
    {
        AddNewItemEvent?.Invoke(text);
    }
}
