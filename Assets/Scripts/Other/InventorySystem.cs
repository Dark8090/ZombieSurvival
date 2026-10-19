using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    [SerializeField] private CharacterBase character;

    [SerializeField] private List<PassiveItem> passiveItemsList = new List<PassiveItem>();
    [SerializeField] private List<WeaponBase> weaponsList = new List<WeaponBase>();

    [SerializeField] private List<Slot> weaponSlot;
    [SerializeField] private List<Slot> itemsSlot;

    public List<PassiveItem> PassiveItemsList => passiveItemsList;
    public List<WeaponBase> WeaponsList => weaponsList;

    // Свободное место определяется по количеству предметов в списке, а не по флагу Slot.isEmpty.
    public bool HasFreeWeaponSlot => weaponsList.Count < weaponSlot.Count;
    public bool HasFreePassiveSlot => passiveItemsList.Count < itemsSlot.Count;

    private void Awake()
    {
        Instance = this;
    }

    // Поиск всегда идёт по ID. Раньше где-то сравнивались ID, а где-то ссылки на ScriptableObject.
    public WeaponBase FindWeapon(int id)
    {
        foreach (var item in weaponsList)
        {
            if (item != null && item.WeaponData.ID == id) return item;
        }
        return null;
    }

    public PassiveItem FindPassiveItem(int id)
    {
        foreach (var item in passiveItemsList)
        {
            if (item != null && item.PassiveItemData.ID == id) return item;
        }
        return null;
    }

    // Проверка включает наличие свободного слота, поэтому отдельные проверки в UIManager больше не нужны.
    public bool CanAddWeapon(WeaponBase weapon)
    {
        return weapon != null && HasFreeWeaponSlot && FindWeapon(weapon.WeaponData.ID) == null;
    }

    public bool CanAddPassiveItem(PassiveItem passiveItem)
    {
        return passiveItem != null && HasFreePassiveSlot && FindPassiveItem(passiveItem.PassiveItemData.ID) == null;
    }

    public void AddWeaponItem(WeaponBase weaponBase)
    {
        if (!CanAddWeapon(weaponBase)) return;

        weaponsList.Add(weaponBase);
        UpdateInventory();
    }

    public void AddPassiveItem(PassiveItem passiveItem)
    {
        if (!CanAddPassiveItem(passiveItem)) return;

        passiveItemsList.Add(passiveItem);
        UpdateInventory();
        character.PlayerStats.RecalculateStats();
    }

    // Повышение уровня вынесено сюда, чтобы после улучшения пассивки статы игрока пересчитывались всегда.
    public bool LevelUpWeapon(WeaponBase weapon)
    {
        return weapon != null && weapon.TryLevelUp();
    }

    public bool LevelUpPassiveItem(PassiveItem passiveItem)
    {
        if (passiveItem == null || !passiveItem.TryLevelUp()) return false;

        character.PlayerStats.RecalculateStats();
        return true;
    }

    // Слоты всегда перерисовываются целиком по индексам списков (UpdateInventory), поэтому рассинхрона быть не может.
    // Объект сначала отключается: Destroy срабатывает только в конце кадра, и до этого он продолжал бы работать.
    public void RemoveWeaponItem(WeaponBase weaponBase)
    {
        if (!weaponsList.Remove(weaponBase)) return;

        UpdateInventory();
        weaponBase.gameObject.SetActive(false);
        Destroy(weaponBase.gameObject);
    }

    public void RemovePassiveItem(PassiveItem passiveItem)
    {
        if (!passiveItemsList.Remove(passiveItem)) return;

        UpdateInventory();
        character.PlayerStats.RecalculateStats();
        passiveItem.gameObject.SetActive(false);
        Destroy(passiveItem.gameObject);
    }

    // Возвращает пассивный предмет, необходимый для эволюции, если все условия выполнены, иначе null.
    // Заменяет дублирующиеся проверки в CanWeaponEvolution и в LevelUpSystemNew.
    public PassiveItem FindEvolutionPassive(WeaponBase weapon)
    {
        WeaponData data = weapon.WeaponData;

        if (!data.CanEvolution || data.EvolutionWeapon == null || data.RequiredPassiveItemToEvolution == null) return null;
        if (!weapon.IsMaxLevel) return null;

        PassiveItem passive = FindPassiveItem(data.RequiredPassiveItemToEvolution.ID);
        return passive != null && passive.IsMaxLevel ? passive : null;
    }

    public void UpdateInventory()
    {
        foreach (var item in weaponSlot)
        {
            if (item != null) item.ClearSlot();
        }
        foreach (var item in itemsSlot)
        {
            if (item != null) item.ClearSlot();
        }

        for (int i = 0; i < weaponsList.Count && i < weaponSlot.Count; i++)
        {
            weaponSlot[i].SetSlot(weaponsList[i].WeaponData.Sprite);
        }
        for (int i = 0; i < passiveItemsList.Count && i < itemsSlot.Count; i++)
        {
            itemsSlot[i].SetSlot(passiveItemsList[i].PassiveItemData.Sprite);
        }
    }
}
