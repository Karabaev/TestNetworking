using Aboba.New.Fight.World;
using Aboba.New.Framework;

namespace Aboba.New.Fight
{
  public class UnitDescriptionPresenter : Presenter<UnitModel, UnitDescriptionView>
  {
    public override void OnStart()
    {
      View.Count = Model.Count.Value;
      View.MaxHealth = Model.MaxHealth;
      View.CurrentHealth = Model.CurrentHealth.Value;
      View.Damage = Model.Damage.Value;
    }

    public UnitDescriptionPresenter(UnitModel model, UnitDescriptionView view) : base(model, view) { }
  }
}