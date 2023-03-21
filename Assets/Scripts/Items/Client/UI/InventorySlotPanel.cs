using Aboba.Items.Common.Model;
using Aboba.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Aboba.Items.Client.UI
{
  public class InventorySlotPanel : MonoBehaviour
  {
    [SerializeField, HideInInspector]
    private Image _icon = null!;
    [SerializeField, HideInInspector]
    private TMP_Text _countLabel = null!;
    
    public InventorySlot Item
    {
      set
      {
        if(value.Item == null)
        {
          _icon.sprite = null!;
          _countLabel.text = string.Empty;
          return;
        }
        
        _icon.sprite = value.Item.Icon;
        _countLabel.text = value.Count.ToString();
      }
    }

    private void OnValidate()
    {
      _icon = this.RequireComponentInChild<Image>("Icon");
      _countLabel = this.RequireComponentInChild<TMP_Text>("CountText");
    }
  }
}