using UnityEngine;
using VContainer.Unity;

namespace Aboba
{
  public class ObjectInjector : MonoBehaviour
  {
    [SerializeField]
    private LifetimeScope _lifetimeScope = null!;
    
    private void Awake() => _lifetimeScope.Container.InjectGameObject(gameObject);

    private void OnValidate() => _lifetimeScope = FindObjectOfType<LifetimeScope>();
  }
}