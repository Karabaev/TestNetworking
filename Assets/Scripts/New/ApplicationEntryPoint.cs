using Aboba.New.StateMachine;
using Aboba.New.StateMachine.States;
using VContainer;
using VContainer.Unity;

namespace Aboba.New
{
  public class ApplicationEntryPoint : LifetimeScope
  {
    protected override void Awake()
    {
      base.Awake();
      DontDestroyOnLoad(this);
    }

    protected override void Configure(IContainerBuilder builder)
    {
      builder.Register<ApplicationStateMachine>(Lifetime.Singleton);
      builder.Register<BootstrapState>(Lifetime.Singleton);
      builder.Register<GameBootstrapState>(Lifetime.Singleton);
      builder.Register<GameLoopState>(Lifetime.Singleton);
      builder.Register<SceneService>(Lifetime.Singleton);
    }

    private void Start()
    {
      Container.Resolve<ApplicationStateMachine>().EnterAsync<BootstrapState>();
    }
  }
}