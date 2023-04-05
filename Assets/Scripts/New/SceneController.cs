using VContainer.Unity;

namespace Aboba.New
{
  public abstract class SceneController : LifetimeScope
  {
    protected override void Awake()
    {
      base.Awake();

      if(Parent != null)
        Parent.Container.Inject(this);
    }
  }
}