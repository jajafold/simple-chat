using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Messages;

namespace Infrastructure.Models;

public static class MessagesViewModelExtensions
{
    public static MessagesViewModel ToMessagesViewModel(this IEnumerable<Message> messages)
    {
        var messageModels =
            messages.Select(x => new MessageViewModel<IMessage> {Name = x.Name, Content = x});
        return new MessagesViewModel {Messages = messageModels.ToArray(), TimeStamp = DateTime.Now.Ticks};
    }
}

public class MessagesViewModel
{
    public MessageViewModel<IMessage>[] Messages { get; set; }
    public long TimeStamp { get; set; }
}

public class MessageViewModel<T> where T : IMessage
{
    public string Name { get; set; }
    public T Content { get; set; }
}