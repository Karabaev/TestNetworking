using Aboba.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Aboba.Characters.Client.UI
{
  public class CharacterStatusBar : MonoBehaviour
  {
    [SerializeField, HideInInspector]
    private TMP_Text _nameText = null!;
    [SerializeField, HideInInspector]
    private Image _hpBar = null!;

    public string Name
    {
      set => _nameText.text = value;
    }

    public void UpdateHp(float currentHp, float maxHp) => _hpBar.fillAmount = currentHp / maxHp;

    private void Update()
    {
      var mainCamera = Camera.main!;

      if(!mainCamera)
        return;

      transform.forward = MathUtils.Direction(mainCamera.transform.position, transform.position);
    }

    private void OnValidate()
    {
      _nameText = this.RequireComponentInChildren<TMP_Text>();
      _hpBar = this.RequireComponentInChild<Image>("HpBar");
    }
  }
}