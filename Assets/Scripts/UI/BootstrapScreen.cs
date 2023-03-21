using Aboba.Utils;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Aboba.UI
{
  public class BootstrapScreen : MonoBehaviour
  {
    [SerializeField, HideInInspector]
    private Button _serverButton = null!;
    [SerializeField, HideInInspector]
    private Button _hostButton = null!;
    [SerializeField, HideInInspector]
    private Button _clientButton = null!;
    
    [Inject]
    private NetworkManager _networkManager = null!;
    
    private void Awake()
    {
      _serverButton.onClick.AddListener(StartServer);
      _hostButton.onClick.AddListener(StartHost);
      _clientButton.onClick.AddListener(StartClient);
    }
    
    private void StartServer() => _networkManager.StartServer();

    private void StartHost() => _networkManager.StartHost();

    private void StartClient() => _networkManager.StartClient();

    private void OnValidate()
    {
      _serverButton = this.RequireComponentInChild<Button>("ServerButton");
      _hostButton = this.RequireComponentInChild<Button>("HostButton");
      _clientButton = this.RequireComponentInChild<Button>("ClientButton");
    }
  }
}