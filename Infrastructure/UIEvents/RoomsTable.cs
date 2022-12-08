using System;
using Infrastructure.Models;

namespace Infrastructure.UIEvents;

public static class RoomsEvents
{
    public delegate void RoomsTableChangeHandler(RoomsTableChangeEventArgs e);

    public static event RoomsTableChangeHandler? RoomsTableChange;

    public static void OnRoomsTableChange(RoomsTableChangeEventArgs e)
    {
        RoomsTableChange?.Invoke(e);
    }
}

public class RoomsTableChangeEventArgs : EventArgs
{
    public RoomViewModel[] Rooms;
}