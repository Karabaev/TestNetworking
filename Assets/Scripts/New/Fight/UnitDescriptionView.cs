using TMPro;
using UnityEngine;

namespace Aboba.New.Fight
{
  public class UnitDescriptionView : MonoBehaviour
  {
    private TMP_Text _countText = null!;
    private TMP_Text _maxHealthText = null!;
    private TMP_Text _currentHealthText = null!;
    private TMP_Text _damageText = null!;

    public int Count
    {
      set => _countText.text = value.ToString();
    }

    public float MaxHealth
    {
      set => _maxHealthText.text = value.ToString();
    }
    
    public float CurrentHealth
    {
      set => _currentHealthText.text = value.ToString();
    }
    
    public float Damage
    {
      set => _damageText.text = value.ToString();
    }
  }
}