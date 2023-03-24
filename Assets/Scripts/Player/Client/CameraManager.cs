using Aboba.Infrastructure;
using Aboba.Utils;
using Cinemachine;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Aboba.Player.Client
{
  [UsedImplicitly]
  public class CameraManager
  {
    private readonly FromResourceFactory _fromResourceFactory;

    public Camera MainCamera { get; private set; } = null!;
    
    public async UniTask CreateCameraAsync(Transform followTo)
    {
      MainCamera = await _fromResourceFactory.CreateAsync<Camera>("pfCamera");
      MainCamera.RequireComponent<CinemachineVirtualCamera>().Follow = followTo;
    }

    public CameraManager(FromResourceFactory fromResourceFactory) => _fromResourceFactory = fromResourceFactory;
  }
}