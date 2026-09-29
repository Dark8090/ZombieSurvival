using UnityEngine;

public class EvolutionItemSystem : MonoBehaviour
{
    public void EvolutionWeapon(WeaponBase evolvingWeapon, PassiveItem requiredPassiveItem) // 1 - Оружие для Эволюции, 2 - требуемый пассивный предмет для эволюции оружия
    {
        if (evolvingWeapon == null || !evolvingWeapon.WeaponData.CanEvolution) return;
        if (evolvingWeapon.Level != evolvingWeapon.WeaponData.MaxLevel || requiredPassiveItem.Level != requiredPassiveItem.PassiveItemData.MaxLevel) return;
        if (InventorySystem.Instance.HasPassiveItem(evolvingWeapon.WeaponData.RequiredPassiveItemToEvolution))
        {
            InventorySystem.Instance.RemoveWeaponItem(evolvingWeapon);
            InventorySystem.Instance.RemovePassiveItem(requiredPassiveItem);

            // заменил на метод из UIManager
            //GameObject gameObject = Instantiate(evolvingWeapon.WeaponData.EvolutionWeapon.WeaponPrefab, GameManager.Instance.WeaponItemBox.transform); 
            //gameObject.name = evolvingWeapon.WeaponData.EvolutionWeapon.WeaponName;
            //InventorySystem.Instance.AddWeaponItem(gameObject.GetComponent<WeaponBase>()); 

            GameObject gameObject = evolvingWeapon.WeaponData.EvolutionWeapon.WeaponPrefab;
            UIManager.Instance.CreateObject(gameObject);
            
        }
    }
}
