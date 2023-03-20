using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Aboba.Infrastructure
{
  [UsedImplicitly]
  public class FromResourceFactory
  {
    private readonly IObjectResolver _objectResolver;
    private readonly ResourceService _resourceService;

    public async UniTask<T> CreateAsync<T>(string path) where T : Object
    {
      var resource = await LoadResourceAsync<T>(path);
      return Instantiate(resource);
    }

    public async UniTask<IReadOnlyList<T>> CreateAsync<T>(string path, int instancesCount) where T : Object
    {
      var prefab = await LoadResourceAsync<T>(path);
      var result = new List<T>(instancesCount);

      for(var i = 0; i < instancesCount; i++)
        result.Add(Instantiate(prefab));

      return result;
    }

    private async UniTask<T> LoadResourceAsync<T>(string path) where T : Object
    {
      var resource = await _resourceService.LoadAsync<T>(path);

      if(!resource)
        throw new NullReferenceException($"Resource was not loaded. Resource={path}");

      return resource;
    }

    private T Instantiate<T>(T prefab) where T : Object
    {
      var instance = Object.Instantiate(prefab);

      if(instance is GameObject go)
        _objectResolver.InjectGameObject(go);
      else if(instance is Component component)
        _objectResolver.InjectGameObject(component.gameObject);

      instance.name = prefab.name;
      return instance;
    }

    public FromResourceFactory(IObjectResolver objectResolver, ResourceService resourceService)
    {
      _objectResolver = objectResolver;
      _resourceService = resourceService;
    }
  }
}