using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image image;
    public bool isEmpty = true;

    private Sprite defaultSprite;

    private void Start()
    {
        defaultSprite = image.sprite;
    }


    public void SetSlot(Sprite sprite)
    {
        image.sprite = sprite;
        isEmpty = false;
    }
    public void ClearSlot()
    {
        image.sprite = defaultSprite;
        isEmpty = true;
    }
}
