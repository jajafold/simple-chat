﻿using System;
using Infrastructure.Messages;
using Infrastructure.Models;

namespace Infrastructure.UIEvents;

public static class ChatEvents
{
    public delegate void ChatMessagesChangeHandler(ChatMessagesChangeEventArgs e);
    public delegate void ChatUsersChangeHandler(ChatUsersChangeEventArgs e);
    public delegate void ChatWindowClosedHandler();

    public static event ChatMessagesChangeHandler? ChatMessagesChange;

    public static void OnChatMessagesChange(ChatMessagesChangeEventArgs e)
    {
        ChatMessagesChange?.Invoke(e);
    }

    public static event ChatUsersChangeHandler? ChatUsersChange;
    public static void OnChatUsersChange(ChatUsersChangeEventArgs e)
    {
        ChatUsersChange?.Invoke(e);
    }

    public static event ChatWindowClosedHandler? ChatWindowClosed;
    public static void OnChatWindowClosed()
    {
        ChatWindowClosed?.Invoke();
    }
}

public class ChatMessagesChangeEventArgs : EventArgs
{
    public MessageViewModel<IMessage>[] Messages;
}

public class ChatUsersChangeEventArgs : EventArgs
{
    public string[] Users;
}