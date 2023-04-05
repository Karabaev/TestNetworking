using TMPro;
using UnityEngine;

namespace Aboba.New.Fight.World
{
  public class UnitView : MonoBehaviour
  {
    private TMP_Text _countText = null!;

    public int Count
    {
      set => _countText.text = value.ToString();
    }
  }
}