using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject player;
    [SerializeField] private SliderLevelText sliderLevel;
    [SerializeField] private MoneySystem moneySystem;


    private void Awake()
    {
        Instance = this;
    }

    public void CreateObject(GameObject gameObject)
    {
        // TODO: Сделать проверку на наличие УЖЕ компонента
        if (gameObject.TryGetComponent(out WeaponBase weapon) && InventorySystem.Instance.CanAddWeapon(weapon))
        {
            InventorySystem.Instance.AddWeaponItem(weapon);
            GameObject newObject = Instantiate(gameObject, player.transform);
            newObject.name = gameObject.name;
        }
        else if (gameObject.TryGetComponent(out PassiveItem passiveItem) && InventorySystem.Instance.CanAddPassiveItem(passiveItem))
        {
            InventorySystem.Instance.AddPassiveItem(passiveItem);
            GameObject newObject = Instantiate(gameObject, player.transform);
            newObject.name = gameObject.name;
        }
        else print("���������� ������� �������");
    }

    public void UpdateSlider(int currentExperience, int maxExperience, int level)
    {
        sliderLevel.SetSlider(currentExperience, maxExperience, level);
    }
    public void UpdateMoney(int count)
    {
        moneySystem.AddMoney(count);
    }

}
