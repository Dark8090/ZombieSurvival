using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUpItem : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemText;

    private Button itemButton;

    public void Init()
    {
        itemButton = GetComponent<Button>();
    }

    public void SetItemImage(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }
    public void SetItemText(string text)
    {
        itemText.text = text;
    }
    public void ClearEventItemButton()
    {
        itemButton.onClick.RemoveAllListeners();
    }
    public void AddEventItemButton(UnityAction newEvent)
    {
        itemButton.onClick.AddListener(newEvent.Invoke);
    }
}
