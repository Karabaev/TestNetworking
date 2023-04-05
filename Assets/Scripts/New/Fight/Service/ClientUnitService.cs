using System.Collections.Generic;
using Aboba.New.Framework;

namespace Aboba.New.Fight.Service
{
  /// <summary>
  /// Методы вызываются из серверных команд.
  /// </summary>
  public class ClientUnitService
  {
    private readonly Dictionary<string, UnitEntity> _units = new();

    public void CreateUnit(string id)
    {
      var unit = new UnitEntity("Monster", 10, 100, 100, 30);
      unit.OnCreated();
      _units[id] = unit;
    }

    public void DestroyUnit(string id)
    {
      _units[id].Dispose();
      _units.Remove(id);
    }
  }

  public class PresenterManager
  {
    private readonly Dictionary<string, IPresenter> _presenters = new();
    private readonly Dictionary<string, object> _models = new();

    public void Create()
    {
      
    }
  }
}