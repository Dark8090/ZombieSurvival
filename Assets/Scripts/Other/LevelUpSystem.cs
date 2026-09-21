using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSystem : MonoBehaviour
{
    //[SerializeField] private List<PassiveItem> passiveItemsList;
    //[SerializeField] private List<WeaponBase> weaponBaseList;
    [SerializeField] private List<GameObject> prefabsAllItems;
    [SerializeField] private List<GameObject> slotsList;

    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotPrefabParent;

    private int countButon = 3;
    private List<GameObject> gameObjects;

    private void Start()
    {
        gameObjects = new List<GameObject>(countButon);

        int count = 0;
        for (int i = 0; i < countButon; i++)
        {
            for (int j = 0; j < countButon; j++)
            {
                int randomObject = Random.Range(0, prefabsAllItems.Count);
                bool tempBool = true;
                count++;
                foreach (GameObject T in gameObjects)
                {
                    if (T.name == prefabsAllItems[randomObject].name)
                    {
                        j = 0;
                        tempBool = false;
                        break;
                    }
                }

                if (tempBool == false) continue;

                GameObject item = Instantiate(slotPrefab, slotPrefabParent);
                item.name = $"LevelUpItem ({i})";
                slotsList.Add(item);
                LevelUpItem levelUpItem = item.GetComponent<LevelUpItem>();
                levelUpItem.Init();

                if (prefabsAllItems[randomObject].TryGetComponent(out WeaponBase weapon))
                {
                    levelUpItem.SetItemText(weapon.WeaponData.Description);
                    levelUpItem.SetItemImage(weapon.WeaponData.Sprite);

                }
                if (prefabsAllItems[randomObject].TryGetComponent(out PassiveItem passiveItem))
                {
                    levelUpItem.SetItemText(passiveItem.PassiveItemData.Description);
                    levelUpItem.SetItemImage(passiveItem.PassiveItemData.Sprite);
                }

                levelUpItem.ClearEventItemButton();
                levelUpItem.AddEventItemButton(() => UpgradeObject(prefabsAllItems[randomObject]));
                levelUpItem.AddEventItemButton(() => CreateObject(prefabsAllItems[randomObject]));
                levelUpItem.AddEventItemButton(() => transform.GetChild(0).gameObject.SetActive(false));

                gameObjects.Add(prefabsAllItems[randomObject]);
                break;
            }
        }
    }
    

    private void CreateObject(GameObject gameObject)
    {
        UIManager.Instance.CreateObject(gameObject);
    }
    private void UpgradeObject(GameObject gameObject)
    {
        UIManager.Instance.UpgradeObject(gameObject);
    }
}
