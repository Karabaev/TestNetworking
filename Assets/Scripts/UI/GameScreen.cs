using Aboba.Items.Client.UI;
using Aboba.Utils;
using Cysharp.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using VContainer;

namespace Aboba.UI
{
  public class GameScreen : MonoBehaviour
  {
    [SerializeField, HideInInspector]
    private TMP_Text _clientIdText = null!;
    [SerializeField, HideInInspector]
    private InventoryPanel _inventoryPanel = null!;
    
    [Inject]
    private NetworkManager _networkManager = null!;

    private void Start()
    {
      _inventoryPanel.InitializeAsync().Forget();
      _clientIdText.text = _networkManager.LocalClientId.ToString();
    }

    private void OnValidate()
    {
      _clientIdText = this.RequireComponentInChild<TMP_Text>("ClientIdLabel");
      _inventoryPanel = this.RequireComponentInChildren<InventoryPanel>();
    }
  }
}