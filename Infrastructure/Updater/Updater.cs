using System.Collections.Generic;

namespace Infrastructure.Updater;

public class Updater<TItem>
{
    private readonly IUpdatable<TItem> _source;

    public Updater(IUpdatable<TItem> source)
    {
        _source = source;
    }

    public void Update(IEnumerable<TItem> collection)
    {
        _source.Update(collection);
    }
}