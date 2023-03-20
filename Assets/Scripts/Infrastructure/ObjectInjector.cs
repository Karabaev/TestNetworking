using UnityEngine;
using VContainer.Unity;

namespace Aboba.Infrastructure
{
  public class ObjectInjector : MonoBehaviour
  {
    [SerializeField]
    private LifetimeScope _lifetimeScope = null!;
    
    private void Awake()
    {
      _lifetimeScope = FindObjectOfType<LifetimeScope>();
      _lifetimeScope.Container.InjectGameObject(gameObject);
    }
  }
}