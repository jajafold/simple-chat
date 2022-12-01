using System.Collections.Generic;

namespace Infrastructure.Updater;

public interface IUpdatable<in TItem>
{
    public void Update(IEnumerable<TItem> items);
}