using Aboba.Utils;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Aboba.UI
{
  [UsedImplicitly]
  public class ScreenService
  {
    private readonly UIService _uiService;
    
    private MonoBehaviour _currentScreen = null!;

    public void Initialize()
    {
      _currentScreen = Object.FindObjectOfType<BootstrapScreen>();
    }
    
    public async UniTask OpenScreenAsync(string resource)
    {
      var previousScreen = _currentScreen;
      _currentScreen = await _uiService.CreateAsync<MonoBehaviour>(resource);

      if(previousScreen)
        previousScreen.DestroyObject();
    }

    public ScreenService(UIService uiService) => _uiService = uiService;
  }
}