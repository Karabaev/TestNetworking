using System.Collections.Generic;
using System.Linq;
using Aboba.Infrastructure;
using Aboba.Utils;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Aboba.UI
{
  [UsedImplicitly]
  public class UIService
  {
    private readonly FromResourceFactory _fromResourceFactory;
    private Dictionary<GameObject, int> _uiElements = new();

    public Canvas MainCanvas { get; }

    public Vector2 SafeAreaSize => new(Screen.width, Screen.height);

    public UniTask InitializeAsync()
    {
      foreach(var element in _uiElements)
      {
        element.Key.SetActive(false);
        Object.Destroy(element.Key);
      }
      _uiElements.Clear();
      return UniTask.CompletedTask;
    }

    public async UniTask<T> CreateAsync<T>(string resource, int priority = -1) where T : MonoBehaviour
    {
      var instance = await _fromResourceFactory.CreateAsync<T>(resource);
      Attach(instance, priority);
      return instance;
    }

    public void Remove(GameObject element)
    {
      element.SetActive(false);
      Detach(element);
      element.DestroyObject();
    }

    private void Attach(MonoBehaviour element, int priority = -1)
    {
      element.transform.SetParent(MainCanvas.transform, false);
      element.transform.localScale = Vector3.one;
      _uiElements[element.gameObject] = priority;

      if(priority == -1)
        element.transform.SetAsFirstSibling();
      else
        SortElements();
    }

    private void Detach(GameObject element) => _uiElements.Remove(element);

    private void SortElements()
    {
      _uiElements = _uiElements.OrderBy(e => e.Value)
                               .ToDictionary(e => e.Key, e => e.Value);

      foreach(var element in _uiElements)
        element.Key.transform.SetAsLastSibling();
    }

    public UIService(FromResourceFactory fromResourceFactory, Canvas canvas)
    {
      _fromResourceFactory = fromResourceFactory;
      MainCanvas = canvas;
    }
  }
}