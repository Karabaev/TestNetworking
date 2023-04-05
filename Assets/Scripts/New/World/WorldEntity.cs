using Aboba.New.Framework;
using UnityEngine;

namespace Aboba.New.World
{
  public class WorldEntity : Entity<WorldModel, WorldView, WorldPresenter>
  {
    private readonly Context _context;
    
    protected override WorldModel CreateModel() => new();

    protected override WorldView CreateView() => Utils.Utils.NewObject<WorldView>("World", _context.Root);

    protected override WorldPresenter CreatePresenter(WorldModel model, WorldView view) => new(model, view);
    
    public WorldEntity(Context context) => _context = context;

    public struct Context
    {
      public Transform Root { get; set; }
    }
  }

  public class WorldModel
  {
  }

  public class WorldView : MonoBehaviour
  {
  }

  public class WorldPresenter : Presenter<WorldModel, WorldView>
  {
    public WorldPresenter(WorldModel model, WorldView view) : base(model, view) { }
  }
}