using System;

namespace Aboba.New.Framework.Reactive
{
  public class ReactiveProperty<T>
  {
    private T _value;

    public event Action<T>? Changed;

    public T Value
    {
      get => _value;
      set
      {
        _value = value;
        Changed?.Invoke(_value);
      }
    }

    public ReactiveProperty(T value) => _value = value;
  }
}