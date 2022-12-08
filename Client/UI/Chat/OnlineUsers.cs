using System.Collections.Generic;
using System.Windows.Forms;
using Infrastructure.Updater;

namespace Chat.UI.Chat
{
    public class OnlineUsers : IUpdatable<string>
    {
        private readonly ListBox _source;

        public OnlineUsers(ListBox source)
        {
            _source = source;
        }

        public void Update(IEnumerable<string> items)
        {
            _source.Items.Clear();
            foreach (var item in items)
                _source.Items.Add(item);
        }
    }
}