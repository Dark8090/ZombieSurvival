using UnityEngine;

[CreateAssetMenu(fileName = "NewRangedWeapon", menuName = "Weapons/Create Ranged Weapon Data")]
public class RangedWeaponData : WeaponData
{
    [Header("Settings Ranged Weapon")]
    public int AttackRange;
    public float RadiousZone;
    public float ZoneDuration;
    public GameObject BulletPrefab;
    
}
