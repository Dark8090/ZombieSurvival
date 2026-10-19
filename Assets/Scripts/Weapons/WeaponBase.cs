using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData WeaponData;
    public int Level { get; set; }

    // Безопасный максимальный уровень: значение из данных не может быть меньше 1.
    public int MaxLevel => Mathf.Max(1, WeaponData.MaxLevel);
    public bool IsMaxLevel => Level >= MaxLevel;

    public virtual void Attack() { }
    public virtual void Attack(Vector3 positon) { }
    public virtual float GetDamage()
    {
        bool isCritical = Random.value < (WeaponData.CriticalChance / 100f);
        float multiplier = isCritical ? WeaponData.CriticalMultiplier : 1f;
        return WeaponData.BaseDamage * multiplier;
    }

    // Уровень задаётся в Awake, а не в Start: наследники (Whip, Knife, Pike) объявляют свой private Start,
    // который скрывал базовый, и Level у них оставался 0. Awake вызывается сразу при Instantiate.
    protected virtual void Awake()
    {
        Level = Mathf.Clamp(WeaponData.Level, 1, MaxLevel);
    }

    protected virtual void Start() { }

    // Единая точка повышения уровня с ограничением по MaxLevel.
    public bool TryLevelUp()
    {
        if (IsMaxLevel) return false;
        Level++;
        return true;
    }
}