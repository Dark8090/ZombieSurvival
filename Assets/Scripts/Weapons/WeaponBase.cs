using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData WeaponData;
    public int Level { get; set; }
    public virtual void Attack() { }
    public virtual void Attack(Vector3 positon) { }
    public virtual float GetDamage()
    {
        bool isCritical = Random.value < (WeaponData.CriticalChance / 100f); // ¬ инспекторе указываем % CriticalChance, например 20% будет = Random.value(от 0 до 1) < 0.2
        float multiplier = isCritical ? WeaponData.CriticalMultiplier : 1f;
        return WeaponData.BaseDamage * multiplier;
    }

    protected virtual void Start()
    {
        Level = WeaponData.Level;
    }
}
