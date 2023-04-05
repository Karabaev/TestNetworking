using Aboba.New.Framework;

namespace Aboba.New
{
  public class ApplicationEntity : Entity<ApplicationModel, ApplicationView, ApplicationPresenter>
  {
    public override void OnCreated()
    {
      base.OnCreated();
      
      
    }

    protected override ApplicationModel CreateModel() => new();

    protected override ApplicationView CreateView() => new();

    protected override ApplicationPresenter CreatePresenter(ApplicationModel model, ApplicationView view) => new(model, view);
  }
  
  public class ApplicationModel { }

  public class ApplicationView { }
  
  public class ApplicationPresenter : Presenter<ApplicationModel, ApplicationView>
  {
    public ApplicationPresenter(ApplicationModel model, ApplicationView view) : base(model, view) { }
  }
}