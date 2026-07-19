using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image image;
    public bool isEmpty = true;

    
    public void SetSlot(Sprite sprite)
    {
        image.sprite = sprite;
    }
    public void ClearSlot()
    {
        image.sprite = null;
    }
}
