using NUnit.Framework;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    [SerializeField] private CharacterBase character;

    [SerializeField] private List<PassiveItem> passiveItemsList = new List<PassiveItem>();
    [SerializeField] private List<WeaponBase> weaponsList = new List<WeaponBase>();

    [SerializeField] private List<GameObject> weaponSlot = new List<GameObject>();
    [SerializeField] private List<GameObject> itemsSlot = new List<GameObject>();


    public List<PassiveItem> PassiveItemsList => passiveItemsList;
    public List<WeaponBase> WeaponsList => weaponsList;

    private void Awake()
    {
        Instance = this;
    }
    public void AddWeaponItem(WeaponBase weaponBase)
    {
        foreach (var item in weaponSlot)
        {

            if (item.GetComponent<Slot>().isEmpty == true)
            {
                item.GetComponent<Slot>().SetSlot(weaponBase.WeaponData.Sprite);
                item.GetComponent<Slot>().isEmpty = false;
                weaponsList.Add(weaponBase);
                print("Dobavil weapon");
                return;
            }
            //else print($"Slot {item} zanyat");
        }
    }
    public bool CanAddPassiveItem(PassiveItem passiveItem)
    {
        foreach (var item in passiveItemsList)
        {
            if (item.PassiveItemData.ID == passiveItem.PassiveItemData.ID) return false;
        }

        return true;
    }
    public bool CanAddWeapon(WeaponBase weapon)
    {
        foreach (var item in weaponsList)
        {
            if (item.WeaponData.ID == weapon.WeaponData.ID) return false;
        }

        return true;
    }
    public void AddPassiveItem(PassiveItem passiveItem)
    {
        foreach (var item in itemsSlot)
        {
            if (item.GetComponent<Slot>().isEmpty == true)
            {
                item.GetComponent<Slot>().SetSlot(passiveItem.PassiveItemData.Sprite);
                item.GetComponent<Slot>().isEmpty = false;
                passiveItemsList.Add(passiveItem);
                character.PlayerStats.RecalculateStats();
                print("Dobavil passiveItem");
                return;
            }
            //else print($"Slot {item} zanyat" );
        }
    }

    public bool CanUpgradeWeaponItem(WeaponBase weapon)
    {
        foreach (var item in weaponsList)
        {
            if (item.WeaponData.ID == weapon.WeaponData.ID) return true;
        }
        return false;
    }
    public bool CanUpgradePassiveItem(PassiveItem passiveItem)
    {
        foreach (var item in passiveItemsList)
        {
            if (item.PassiveItemData.ID == passiveItem.PassiveItemData.ID) return true;
        }
        return false;
    }

    public void RemoveWeaponItem(WeaponBase weaponBase)
    {
        weaponsList.Remove(weaponBase);
    }
    public void RemovePassiveItem(PassiveItem passiveItem)
    {
        passiveItemsList.Remove(passiveItem);
    }
    
}
