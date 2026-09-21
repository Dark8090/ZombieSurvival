using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    //[SerializeField] private List<PassiveItem> passiveItems = new(6);
    //[SerializeField] private List<WeaponBase> weaponBases = new(6);

    private Rigidbody2D rb;
    private PlayerStats playerStats;
    public CharacterData CharacterData { get => characterData; }
    public PlayerStats PlayerStats { get => playerStats; set => value = playerStats; }
    //public IReadOnlyList<PassiveItem> PassiveItems => passiveItems.AsReadOnly();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
    }
    private void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(inputX, inputY) * characterData.MoveSpeed;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickupItem"))
        {
            PickupItem pickupItem = collision.GetComponent<PickupItem>();
            if (pickupItem != null)
            {
                if (pickupItem is ItemGem itemGem)
                {
                    itemGem.SetTarget(transform);
                }
            }
        }
    }




    //public void AddPassiveItem(PassiveItem passiveItem)
    //{
    //    if (passiveItem == null) return;

    //    foreach (var item in passiveItems)
    //    {
    //        if (item.PassiveItemData.Name == passiveItem.PassiveItemData.Name)
    //        {
    //            print($"{item.PassiveItemData.Name} ��� ���� � ������.");
    //            return;
    //        }
    //    }
    //    passiveItems.Add(passiveItem);

    //    playerStats.RecalculateStats();
    //}
    //public void AddWeaponBase(WeaponBase weaponBase)
    //{
    //    weaponBases.Add(weaponBase);
    //}


}
