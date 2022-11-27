namespace chatmana.Models;

public class ResponseViewModel
{
    public IEnumerable<string> UserNames;
    public Guid RoomId;
}

public static class ResponseViewControllerExtensions
{
    public static ResponseViewModel ToResponseViewModel(this IEnumerable<string> names, Guid id)
    {
        return new ResponseViewModel {UserNames = names, RoomId = id};
    }
}