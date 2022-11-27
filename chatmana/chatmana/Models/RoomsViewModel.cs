namespace chatmana.Models;

public static class RoomsViewModelExtensions
{
    public static RoomsViewModel ToHubsViewModel(this IEnumerable<ChatRoom> chatRooms)
    {
        var hubsModels = chatRooms
            .Select(x => new RoomViewModel {ActiveUsers = x.Users.Count, Id = x.Id});
        return new RoomsViewModel {ChatRooms = hubsModels.ToArray()};
    }
}

public class RoomsViewModel
{
    public RoomViewModel[] ChatRooms;
}

public class RoomViewModel
{
    public int ActiveUsers;
    public Guid Id;
}