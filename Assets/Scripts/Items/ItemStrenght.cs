using UnityEngine;

public class ItemStrenght : PassiveItem
{
    public override float GetStrenghtBonus() => PassiveItemData.Might;

}
