using UnityEngine;

[CreateAssetMenu(fileName = "NewMeleeWeapon", menuName = "Weapons/Create Melee Weapon Data")]
public class MeleeWeaponData : WeaponData
{
    [Header("Settings Melee Weapon ")]
    public float AttackRange = 2f;
}
