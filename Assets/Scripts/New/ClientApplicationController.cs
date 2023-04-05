using VContainer;
using VContainer.Unity;

namespace Aboba.New
{
  public class ClientApplicationController : LifetimeScope
  {
    private ApplicationEntity _entity = null!;
    
    protected override void Configure(IContainerBuilder builder)
    {
      base.Configure(builder);
    }
    
    protected override void Awake()
    {
      base.Awake();
      _entity = new ApplicationEntity();
      _entity.OnCreated();
    }
    
    protected override void OnDestroy()
    {
      _entity.Dispose();
      base.OnDestroy();
    }
  }
}