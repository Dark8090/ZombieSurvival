using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUpItem : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemText;

    private Button itemButton;

    // Кнопка кэшируется в Awake. Публичный Init и ленивая инициализация удалены.
    private void Awake()
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

    // Действие подписывается напрямую, без лишней обёртки newEvent.Invoke.
    public void AddEventItemButton(UnityAction newEvent)
    {
        itemButton.onClick.AddListener(newEvent);
    }
}