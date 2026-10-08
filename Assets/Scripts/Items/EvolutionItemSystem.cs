using UnityEngine;

public class EvolutionItemSystem : MonoBehaviour
{
    public static EvolutionItemSystem Instance;
    private void Awake()
    {
        Instance = this;
    }
    //public void EvolutionWeapon(WeaponBase weaponBase, PassiveItem requiredPassiveItem) // 1 - Оружие для Эволюции, 2 - требуемый пассивный предмет для эволюции оружия
    //{
    //    if (weaponBase == null || requiredPassiveItem == null || !weaponBase.WeaponData.CanEvolution) return;
    //    if (weaponBase.Level != weaponBase.WeaponData.MaxLevel || requiredPassiveItem.Level != requiredPassiveItem.PassiveItemData.MaxLevel) return;
    //    if (InventorySystem.Instance.HasPassiveItem(weaponBase.WeaponData.RequiredPassiveItemToEvolution))
    //    {
    //        InventorySystem.Instance.RemoveWeaponItem(weaponBase);
    //        InventorySystem.Instance.RemovePassiveItem(requiredPassiveItem);

    //        // заменил на метод из UIManager
    //        //GameObject gameObject = Instantiate(evolvingWeapon.WeaponData.EvolutionWeapon.WeaponPrefab, GameManager.Instance.WeaponItemBox.transform); 
    //        //gameObject.name = evolvingWeapon.WeaponData.EvolutionWeapon.WeaponName;
    //        //InventorySystem.Instance.AddWeaponItem(gameObject.GetComponent<WeaponBase>()); 

    //        GameObject gameObject = weaponBase.WeaponData.EvolutionWeapon.WeaponPrefab;
    //        UIManager.Instance.CreateObject(gameObject);
    //        //UIManager.Instance.UpdateInventory(); // Обновление UI


    //    }
    //}
    //NEW
    public void EvolutionWeapon(WeaponBase weaponBase, PassiveItem requiredPassiveItem) // 1 - Оружие для Эволюции, 2 - требуемый пассивный предмет для эволюции оружия
    {
        InventorySystem.Instance.RemoveWeaponItem(weaponBase);
        InventorySystem.Instance.RemovePassiveItem(requiredPassiveItem);

        GameObject gameObject = weaponBase.WeaponData.EvolutionWeapon.WeaponPrefab;
        UIManager.Instance.CreateObject(gameObject);
        InventorySystem.Instance.UpdateInventory(); // Обновление Inventory
    }
}
