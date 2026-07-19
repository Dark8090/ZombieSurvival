using UnityEngine;

public class ItemMoveSpeed : PassiveItem
{
    public override float GetMoveSpeedBonus() => PassiveItemData.MoveSpeed;

}
