using System;
using UnityEngine;


public class PassiveItem : MonoBehaviour
{
    [SerializeField] protected PassiveItemData _passiveItemData;
    private int level;
    public int Level { get => level; set => level = value; }
    public PassiveItemData PassiveItemData { get => _passiveItemData; }

    public virtual float GetMaxHealthBonus() => 0f;
    public virtual float GetRegenerationHealthBonus() => 0f;
    public virtual float GetArmorBonus() => 0f;
    public virtual float GetMoveSpeedBonus() => 0f;
    public virtual float GetStrenghtBonus() => 0f;



    protected virtual void Awake()
    {
        level = _passiveItemData.Level;
    }

    //public void AddItem()
    //{
    //    GameManager.Instance?.CharacterBase.AddPassiveItem(this);
    //}
}
