using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Updater;

namespace Chat.UI;

public class RoomsTable : IUpdatable<RoomViewModel>
{
    private readonly DataGridView _source;
    private readonly HashSet<RoomViewModel> _rows = new ();

    public RoomsTable(DataGridView source)
    {
        _source = source;
    }

    public void Update(IEnumerable<RoomViewModel> items)
    {
        foreach (var room in items)
        {
            if (_source.Rows
                .Cast<DataGridViewRow>()
                .Any(row => row.Cells[0].Value.ToString() == room.Name)) continue;

            _source.Rows.Add(room.Name, room.Protection, $"[{room.ActiveUsers} / {room.MaxCapacity}]");
            _rows.Add(room);
        }
    }
}