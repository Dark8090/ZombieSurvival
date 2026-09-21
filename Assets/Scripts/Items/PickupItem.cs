using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] protected PickupsItemData PickupsItemData;

    public virtual void Use() { Destroy(gameObject); }
}
