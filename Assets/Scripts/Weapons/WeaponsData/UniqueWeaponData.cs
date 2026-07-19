using UnityEngine;


[CreateAssetMenu(fileName = "NewUniqueWeapon", menuName = "Weapons/Create Unique Weapon Data")]
public class UniqueWeaponData : WeaponData
{
    [Header("Unique Settings")]
    public GameObject bulletPrefab;
}
