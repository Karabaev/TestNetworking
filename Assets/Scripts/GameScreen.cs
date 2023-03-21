using Aboba.Items.Client.UI;
using Aboba.Utils;
using Cysharp.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Aboba
{
  public class GameScreen : MonoBehaviour
  {
    [SerializeField, HideInInspector]
    private Button _serverButton = null!;
    [SerializeField, HideInInspector]
    private Button _hostButton = null!;
    [SerializeField, HideInInspector]
    private Button _clientButton = null!;
    [SerializeField, HideInInspector]
    private TMP_Text _statusText = null!;
    [SerializeField, HideInInspector]
    private TMP_Text _clientIdText = null!;
    [SerializeField, HideInInspector]
    private InventoryPanel _inventoryPanel = null!;
    
    [Inject]
    private NetworkManager _networkManager = null!;
    
    private void Awake()
    {
      _serverButton.onClick.AddListener(StartServer);
      _hostButton.onClick.AddListener(StartHost);
      _clientButton.onClick.AddListener(StartClient);
    }

    private void StartServer()
    {
      var result = _networkManager.StartServer();
      _statusText.text = result ? "Server started" : "Failed to start server";
      _clientIdText.text = result ? _networkManager.LocalClientId.ToString() : string.Empty;
    }
    
    private void StartHost()
    {
      var result = _networkManager.StartHost();
      _statusText.text = result ? "Host started" : "Failed to start host";
      _clientIdText.text = result ? _networkManager.LocalClientId.ToString() : string.Empty;
      
      _inventoryPanel.InitializeAsync().Forget();
    }
    
    private void StartClient()
    {
      var result = _networkManager.StartClient();
      _statusText.text = result ? "Client started" : "Failed to start client";
      _clientIdText.text = result ? _networkManager.LocalClientId.ToString() : string.Empty;
      
      _inventoryPanel.InitializeAsync().Forget();
    }
    
    private void OnValidate()
    {
      _serverButton = this.RequireComponentInChild<Button>("ServerButton");
      _hostButton = this.RequireComponentInChild<Button>("HostButton");
      _clientButton = this.RequireComponentInChild<Button>("ClientButton");
      _statusText = this.RequireComponentInChild<TMP_Text>("StatusLabel");
      _clientIdText = this.RequireComponentInChild<TMP_Text>("ClientIdLabel");
      _inventoryPanel = this.RequireComponentInChildren<InventoryPanel>();
    }
  }
}