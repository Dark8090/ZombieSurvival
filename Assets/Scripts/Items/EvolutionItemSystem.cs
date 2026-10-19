using UnityEngine;

public class EvolutionItemSystem : MonoBehaviour
{
    public static EvolutionItemSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void EvolutionWeapon(WeaponBase weaponBase, PassiveItem requiredPassiveItem)
    {
        WeaponData data = weaponBase.WeaponData;
        if (data.EvolutionWeapon == null || data.EvolutionWeapon.WeaponPrefab == null) return;

        // Всё нужное читаем до удаления оружия, чтобы не обращаться к объекту, который уничтожается.
        GameObject evolvedPrefab = data.EvolutionWeapon.WeaponPrefab;
        bool consumePassive = data.ConsumePassiveItemOnEvolution;

        // Если эволюционное оружие уже есть в инвентаре, эволюция отменяется, иначе исходное оружие пропало бы зря.
        if (!evolvedPrefab.TryGetComponent(out WeaponBase evolvedWeapon)) return;
        if (InventorySystem.Instance.FindWeapon(evolvedWeapon.WeaponData.ID) != null) return;

        InventorySystem.Instance.RemoveWeaponItem(weaponBase);

        // Пассивный предмет теперь удаляется только если это включено в данных оружия.
        if (consumePassive && requiredPassiveItem != null)
        {
            InventorySystem.Instance.RemovePassiveItem(requiredPassiveItem);
        }

        UIManager.Instance.CreateObject(evolvedPrefab);
    }
}