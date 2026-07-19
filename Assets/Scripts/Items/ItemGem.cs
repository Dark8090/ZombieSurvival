using UnityEngine;

public class ItemGem : PickupItem
{
    public override void Use()
    {
        GameManager.Instance.PlayerStats.AddExperience(PickupsItemData.Count);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Use();
        Destroy(gameObject);
    }
}
