using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Aboba
{
  public class GameScreen : MonoBehaviour
  {
    [SerializeField]
    private Button _serverButton;
    [SerializeField]
    private Button _hostButton;
    [SerializeField]
    private Button _clientButton;
    [SerializeField]
    private TMP_Text _statusText;

    private void Awake()
    {
      _serverButton.onClick.AddListener(StartServer);
      _hostButton.onClick.AddListener(StartHost);
      _clientButton.onClick.AddListener(StartClient);
    }

    private void StartServer()
    {
      var result = NetworkManager.Singleton.StartServer();
      _statusText.text = result ? "Server started" : "Failed to start server";
    }
    
    private void StartHost()
    {
      var result = NetworkManager.Singleton.StartHost();
      _statusText.text = result ? "Host started" : "Failed to start host";
    }
    
    private void StartClient()
    {
      var result = NetworkManager.Singleton.StartClient();
      _statusText.text = result ? "Client started" : "Failed to start client";
    }
  }
}