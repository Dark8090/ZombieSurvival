using Mono.Cecil;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class InventorySystem : MonoBehaviour
//TODO: Сделать метод который будем перебирать имеющиеся листы с оружием и пассивными предметами для того, чтобы найти, есть ли предмет готовый 
// к эволюции, если да, передавать в метод для Эволюции оба класса WeaponBase и PassiveItem

{
    public static InventorySystem Instance;

    [SerializeField] private CharacterBase character;

    [SerializeField] private List<PassiveItem> passiveItemsList = new List<PassiveItem>();
    [SerializeField] private List<WeaponBase> weaponsList = new List<WeaponBase>();

    [SerializeField] private List<Slot> weaponSlot = new List<Slot>();
    [SerializeField] private List<Slot> itemsSlot = new List<Slot>();

    private EvolutionItemSystem evolutionItemSystem = new EvolutionItemSystem();
    public List<PassiveItem> PassiveItemsList => passiveItemsList;
    public List<WeaponBase> WeaponsList => weaponsList;
    private WeaponBase weaponTemp = null;
    private PassiveItem requiredPassiveItemTemp = null;

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

    public void RemoveWeaponItem(WeaponBase weaponBase) //TODO: Сделать отдельный метод который будем заново перебирать Инвентарь (вызывать после Remove)
    {
        int weaponIndex = weaponsList.IndexOf(weaponBase);
        weaponSlot[weaponIndex].ClearSlot();
        GameObject deleteObject = weaponBase.gameObject;
        weaponsList.Remove(weaponBase);
        Destroy(deleteObject);
    }
    public void RemovePassiveItem(PassiveItem passiveItem)
    {
        int passiveItemIndex = passiveItemsList.IndexOf(passiveItem);
        itemsSlot[passiveItemIndex].ClearSlot();
        GameObject deleteObject = passiveItem.gameObject;
        passiveItemsList.Remove(passiveItem);
        Destroy(deleteObject);
    }

    public bool HasPassiveItem(PassiveItemData passiveItemData)
    {
        foreach (var item in passiveItemsList)
        {
            if (item.PassiveItemData.ID == passiveItemData.ID) return true;
        }
        return false;
    }

    public void CanWeaponEvolution(WeaponBase weaponBase, PassiveItem requiredPassiveItem)
    {
        
        foreach (var item in weaponsList)
        {
            if (item.WeaponData.ID != weaponBase.WeaponData.ID) return;
            weaponTemp = item;
        }
        foreach (var item in passiveItemsList)
        {
            if (item.PassiveItemData.ID != requiredPassiveItem.PassiveItemData.ID) return;
            requiredPassiveItemTemp = item;
        }

        evolutionItemSystem.EvolutionWeapon(weaponTemp, requiredPassiveItemTemp);
        weaponTemp = null;
        requiredPassiveItemTemp = null;
    }

}
