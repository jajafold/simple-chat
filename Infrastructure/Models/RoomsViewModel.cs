using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Models;

public static class RoomsViewModelExtensions
{
    public static RoomsViewModel ToHubsViewModel(this IEnumerable<ChatRoom> chatRooms)
    {
        var hubsModels = chatRooms
            .Select(x => new RoomViewModel
            {
                ActiveUsers = x.UsersCount, 
                Id = x.Id,
                Name = x.Name,
                Protection = x.RequiresPassword,
                MaxCapacity = x.MaxUsers
            });
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
    public string Name;
    public bool Protection;
    public int MaxCapacity;
}