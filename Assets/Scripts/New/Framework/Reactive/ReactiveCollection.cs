using System;
using System.Collections.Generic;

namespace Aboba.New.Framework.Reactive
{
  public class ReactiveCollection<TItem>
  {
    private readonly List<TItem> _collection;

    public IReadOnlyList<TItem> Collection => _collection;

    public event Action? CollectionChanged;

    public void Add(TItem item)
    {
      _collection.Add(item);
      CollectionChanged?.Invoke();
    }

    public void AddRange(IEnumerable<TItem> items)
    {
      _collection.AddRange(items);
      CollectionChanged?.Invoke();
    }

    public void Remove(TItem item)
    {
      _collection.Remove(item);
      CollectionChanged?.Invoke();
    }

    public void Reinitialize(IEnumerable<TItem> items)
    {
      _collection.Clear();
      _collection.AddRange(items);
      CollectionChanged?.Invoke();
    }
    
    public TItem this[int index]
    {
      get => _collection[index];
      set
      {
        _collection[index] = value;
        CollectionChanged?.Invoke();
      }
    }

    public ReactiveCollection() => _collection = new List<TItem>();
  }
}