using System.Collections.Generic;
using UnityEngine;

namespace Aboba.New.Framework
{
  public interface IEntity
  {
    void OnCreated();

    void Dispose();
  }
  
  public abstract class Entity<TModel, TView, TPresenter> : IEntity where TPresenter : IPresenter
  {
    protected TModel Model { get; private set; } = default!;
    protected TView View { get; private set; } = default!;
    protected TPresenter Presenter { get; private set; } = default!;
    protected List<IEntity> Children { get; private set; } = default!;

    public virtual void OnCreated()
    {
      Model = CreateModel();
      View = CreateView();
      Presenter = CreatePresenter(Model, View);
      Children = new List<IEntity>();
    }

    public void Dispose()
    {
      foreach(var child in Children)
        child.Dispose();

      Object.Destroy(View as Object);
      Presenter.OnDestroy();
      OnDisposed();
    }
    
    protected virtual void OnDisposed() { }

    protected abstract TModel CreateModel();
    
    protected abstract TView CreateView();
    
    protected abstract TPresenter CreatePresenter(TModel model, TView view);
  }
}