using UnityEngine;
using UnityEngine.UI;
    
public class UIExperienceBar : MonoBehaviour
{
    [SerializeField] private RPGEntity _entity;

    [SerializeField] private RectTransform _expBarArea;
    [SerializeField] private RectTransform _expBarFill;

    [SerializeField] private Text _expBarValues;

    private void Awake()
    {
        _entity.EntityLevel.OnEntityExpGain += OnExpGain;
        UpdateUI();
    }

    private void OnExpGain(object sender, RPGExpGainEventArgs args)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        float expPercent = Mathf.Clamp((float)_entity.EntityLevel.ExpCurrent / _entity.EntityLevel.ExpRequired, 0f, 1f);
        float newRightOffset = _expBarArea.rect.width * (expPercent - 1);
        
        _expBarFill.offsetMax = new Vector2(newRightOffset, _expBarFill.offsetMax.y);

        _expBarValues.text = $"{_entity.EntityLevel.ExpCurrent} / {_entity.EntityLevel.ExpRequired} (Level {_entity.EntityLevel.Level})";
    }
}
