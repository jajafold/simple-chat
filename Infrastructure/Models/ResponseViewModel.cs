using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public class ResponseViewModel
{
    public Guid RoomId;
    public IEnumerable<string> UserNames;
}

public static class ResponseViewControllerExtensions
{
    public static ResponseViewModel ToResponseViewModel(this IEnumerable<string> names, Guid id)
    {
        return new ResponseViewModel {UserNames = names, RoomId = id};
    }
}