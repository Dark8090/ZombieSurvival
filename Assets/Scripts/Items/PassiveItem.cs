using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    [SerializeField] protected PassiveItemData _passiveItemData;
    public int Level { get; set; }
    public PassiveItemData PassiveItemData { get => _passiveItemData; }

    // Безопасный максимальный уровень: у части ассетов MaxLevel = 0, из-за чего предмет сразу считался максимальным.
    public int MaxLevel => Mathf.Max(1, _passiveItemData.MaxLevel);
    public bool IsMaxLevel => Level >= MaxLevel;

    public virtual float GetMaxHealthBonus() => 0f;
    public virtual float GetRegenerationHealthBonus() => 0f;
    public virtual float GetArmorBonus() => 0f;
    public virtual float GetMoveSpeedBonus() => 0f;
    public virtual float GetStrenghtBonus() => 0f;

    // Awake вместо Start: уровень должен быть выставлен сразу при создании, до первого пересчёта статов.
    private void Awake()
    {
        Level = Mathf.Clamp(_passiveItemData.Level, 1, MaxLevel);
    }

    // Единая точка повышения уровня с ограничением по MaxLevel.
    public bool TryLevelUp()
    {
        if (IsMaxLevel) return false;
        Level++;
        return true;
    }
}