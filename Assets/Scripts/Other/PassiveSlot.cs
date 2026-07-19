using UnityEngine;

public class PassiveSlot : MonoBehaviour
{
    [SerializeField] private PassiveItem passiveItem;
    public PassiveItem PassiveItem => passiveItem;
    

    public void AddItem(PassiveItem passiveItem)
    {
        this.passiveItem = passiveItem;
    }
    public void ClearSlot()
    {
       passiveItem = null;
    }
}
