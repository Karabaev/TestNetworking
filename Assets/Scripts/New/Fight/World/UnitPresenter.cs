using Aboba.New.Framework;

namespace Aboba.New.Fight.World
{
  public class UnitPresenter : Presenter<UnitModel, UnitView>
  {
    public override void OnStart()
    {
      Model.Count.Changed += Model_OnCountChanged;
    }

    private void Model_OnCountChanged(int newValue)
    {
      View.Count = newValue;
    }

    public UnitPresenter(UnitModel model, UnitView view) : base(model, view) { }
  }
}