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

    // Проверка выполняется один раз и по префабу, до Instantiate. Раньше проверка дублировалась
    // до и после создания, а при неудаче в сцене оставался лишний объект.
    // Метод возвращает true, если предмет действительно добавлен в инвентарь.
    public bool CreateObject(GameObject prefab)
    {
        if (prefab == null) return false;

        InventorySystem inventory = InventorySystem.Instance;

        if (prefab.TryGetComponent(out WeaponBase weaponPrefab))
        {
            if (!inventory.CanAddWeapon(weaponPrefab)) return false;

            GameObject newObject = Instantiate(prefab);
            newObject.name = prefab.name;
            newObject.transform.SetParent(GameManager.Instance.WeaponItemBox.transform);
            inventory.AddWeaponItem(newObject.GetComponent<WeaponBase>());
            return true;
        }

        if (prefab.TryGetComponent(out PassiveItem passivePrefab))
        {
            if (!inventory.CanAddPassiveItem(passivePrefab)) return false;

            GameObject newObject = Instantiate(prefab);
            newObject.name = prefab.name;
            newObject.transform.SetParent(GameManager.Instance.PassiveItemBox.transform);
            inventory.AddPassiveItem(newObject.GetComponent<PassiveItem>());
            return true;
        }

        return false;
    }

    // Метод UpgradeObject удалён: он нигде не вызывался, а улучшение теперь выполняют
    // InventorySystem.LevelUpWeapon и InventorySystem.LevelUpPassiveItem.

    public void UpdateSlider(int currentExperience, int maxExperience, int level)
    {
        sliderLevel.SetSlider(currentExperience, maxExperience, level);
    }

    public void UpdateMoney(int count)
    {
        moneySystem.AddMoney(count);
    }
}
