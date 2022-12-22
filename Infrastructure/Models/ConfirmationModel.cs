using System;

namespace Infrastructure.Models;

public class ConfirmationModel
{
    public bool NeedsConfirmation;
    public bool CanFit;
    public Guid RoomId;
}

public class ConfirmationResult
{
    public bool Success;
}

public static class ConfirmationExtensions
{
    public static ConfirmationModel ToConfirmationModel(this ChatRoom chatRoom)
    {
        return new ConfirmationModel
        {
            RoomId = chatRoom.Id, 
            NeedsConfirmation = chatRoom.Password != null,
            CanFit = chatRoom.CanFit
        };
    }
}