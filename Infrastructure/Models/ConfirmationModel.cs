using System;

namespace Infrastructure.Models;

public class ConfirmationModel
{
    public Guid RoomId;
    public bool NeedsConfirmation;
}

public class ConfirmationResult
{
    public bool Success;
}

public static class ConfirmationExtensions
{
    public static ConfirmationModel ToConfirmationModel(this ChatRoom chatRoom)
    {
        return new ConfirmationModel {RoomId = chatRoom.Id, NeedsConfirmation = chatRoom.RequiresPassword};
    }
}