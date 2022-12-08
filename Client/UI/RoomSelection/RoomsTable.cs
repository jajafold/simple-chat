using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Infrastructure.Models;
using Infrastructure.Updater;

namespace Chat.UI.RoomSelection;

public class RoomsTable : IUpdatable<RoomViewModel>
{
    private readonly DataGridView _source;

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

            var capacity = room.MaxCapacity > 0 ? room.MaxCapacity.ToString() : "∞";
            var protection = room.Protection ? "С паролем" : "Публичная";
            
            _source.Rows.Add(
                room.Name, 
                protection, 
                $"[{room.ActiveUsers} / {capacity}]",
                room.Id);
        }
    }
}