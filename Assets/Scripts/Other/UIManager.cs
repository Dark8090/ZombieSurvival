using UnityEditor.Rendering;
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
        if (gameObject != null)
        {
            if (gameObject.TryGetComponent(out WeaponBase weaponBase) && InventorySystem.Instance.CanAddWeapon(weaponBase) ||
                gameObject.TryGetComponent(out PassiveItem passiveItemBase) && InventorySystem.Instance.CanAddPassiveItem(passiveItemBase))
            {
                GameObject newObject = Instantiate(gameObject);

                if (newObject.TryGetComponent(out WeaponBase weapon) && InventorySystem.Instance.CanAddWeapon(weapon))
                {
                    InventorySystem.Instance.AddWeaponItem(weapon);
                    newObject.name = gameObject.name;
                    newObject.transform.SetParent(GameManager.Instance.WeaponItemBox.transform);

                }

                else if (newObject.TryGetComponent(out PassiveItem passiveItem) && InventorySystem.Instance.CanAddPassiveItem(passiveItem))
                {
                    InventorySystem.Instance.AddPassiveItem(passiveItem);
                    newObject.name = gameObject.name;
                    newObject.transform.SetParent(GameManager.Instance.PassiveItemBox.transform);
                }
            }

        }


        // TODO: Сделать проверку на наличие УЖЕ компонента

    }
    public void UpgradeObject(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out WeaponBase weapon) && InventorySystem.Instance.CanUpgradeWeaponItem(weapon))
        {
            foreach (var item in InventorySystem.Instance.WeaponsList)
            {
                Debug.Log($"Текущий уровень - {item.GetComponent<WeaponBase>().Level}"); 

                if (item.GetComponent<WeaponBase>().Level < weapon.WeaponData.MaxLevel)
                {
                    item.GetComponent<WeaponBase>().Level++;
                    Debug.Log($"Уровень повышен, текущий уровень - {item.GetComponent<WeaponBase>().Level}, максимальный уровень оружия - {weapon.WeaponData.MaxLevel}");
                    break;
                }
            }
        }
        else if (gameObject.TryGetComponent(out PassiveItem passiveItem) && InventorySystem.Instance.CanUpgradePassiveItem(passiveItem)) 
        {
            foreach (var item in InventorySystem.Instance.PassiveItemsList) //TODO: Добавить Level в PassiveItemData
            {

                if (item.GetComponent<PassiveItem>().Level < passiveItem.PassiveItemData.MaxLevel)
                {
                    item.GetComponent<PassiveItem>().Level++;
                    GameManager.Instance.CharacterBase.PlayerStats.RecalculateStats();
                }
            }
        }

        
    }




    public void UpdateSlider(int currentExperience, int maxExperience, int level)
    {
        sliderLevel.SetSlider(currentExperience, maxExperience, level);
    }
    public void UpdateMoney(int count)
    {
        moneySystem.AddMoney(count);
    }

    //public void TestDestroy(WeaponBase weaponBase)
    //{
    //    InventorySystem.Instance.RemoveWeaponItem(weaponBase);
    //}

}
