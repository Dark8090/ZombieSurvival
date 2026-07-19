using UnityEngine;

public class ItemArmor : PassiveItem
{
    public override float GetArmorBonus() => PassiveItemData.Armor;
    
}
