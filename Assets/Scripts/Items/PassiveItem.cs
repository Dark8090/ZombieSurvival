using System;
using UnityEngine;


public class PassiveItem : MonoBehaviour
{
    [SerializeField] protected PassiveItemData _passiveItemData;
    public PassiveItemData PassiveItemData { get => _passiveItemData; }
 
    public virtual float GetMaxHealthBonus() => 0f;
    public virtual float GetRegenerationHealthBonus() => 0f;
    public virtual float GetArmorBonus() => 0f;
    public virtual float GetMoveSpeedBonus() => 0f;
    public virtual float GetStrenghtBonus() => 0f;



    //public void AddItem()
    //{
    //    GameManager.Instance?.CharacterBase.AddPassiveItem(this);
    //}
}
