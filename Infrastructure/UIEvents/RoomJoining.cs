using System;

namespace Infrastructure.UIEvents;

public static class RoomJoining
{
    public delegate void UserJoinedRoomHandler(UserJoinedRoomEventArgs e);
    public static event UserJoinedRoomHandler? UserJoinedRoom;

    public static void OnUserJoinedRoom(UserJoinedRoomEventArgs e)
    {
        UserJoinedRoom?.Invoke(e);
    }

    public delegate void TryJoinProtectedRoomHandler();
    public static event TryJoinProtectedRoomHandler? TryJoinProtectedRoom;

    public static void OnTryJoinProtectedRoom()
    {
        TryJoinProtectedRoom?.Invoke();
    }

    public delegate void IncorrectPasswordEnteredHandler();
    public static event IncorrectPasswordEnteredHandler? IncorrectPasswordEntered;

    public static void OnIncorrectPasswordEntered()
    {
        IncorrectPasswordEntered?.Invoke();
    }
}

public class UserJoinedRoomEventArgs : EventArgs { }