using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class LevelUpSystem : MonoBehaviour
{
    public static LevelUpSystem Instance;


    //[SerializeField] private List<PassiveItem> passiveItemsList;
    //[SerializeField] private List<WeaponBase> weaponBaseList;
    [SerializeField] private List<GameObject> prefabsAllItems;
    [SerializeField] private List<GameObject> prefabsEvolutionItems;
    [SerializeField] private List<GameObject> slotsList;

    PassiveItem passiveItemToEvolutionTemp;

    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotPrefabParent;

    private int countButon = 3;
    private List<GameObject> gameObjects;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //LevelUp();
    }


    public void LevelUp()
    {
        prefabsEvolutionItems = new List<GameObject>(countButon); // создаем и чистим при каждом вызове
                                                                  //prefabsEvolutionItems.Clear();


        // Этап 1, проверка на возможность эволюции оружия
        foreach (var item in InventorySystem.Instance.WeaponsList)
        {
            //passiveItemToEvolutionTemp = null; // каждую итерацию делаем null для корректной проверки

            if (InventorySystem.Instance.CanWeaponEvolution(item)) // проверяем, можем ли мы эволюционировать оружие, если да,
                                                                   // то префаб эволюционного оружия добавляем в список
            {
                //foreach (var passiveItem in InventorySystem.Instance.PassiveItemsList) // вытаскиваем passiveItem из инвентаря если можно эволюционировать оружие                                                               // и 
                //{
                //if (InventorySystem.Instance.HasPassiveItem(item.WeaponData.RequiredPassiveItemToEvolution)) 
                //{
                //passiveItemToEvolutionTemp = passiveItem;
                prefabsEvolutionItems.Add(item.WeaponData.EvolutionWeapon.WeaponPrefab);
                //break;
                //}
                //}
            }

        }

        for (int i = 0; i < countButon; i++) // создание кнопки
        {
            bool itemEvolutionFound = false;
            passiveItemToEvolutionTemp = null;


            // Этап 2, генерация и заполнение кнопок
            gameObjects = new List<GameObject>(countButon);

            for (int j = 0; j < countButon; j++) // заполнение кнопки
            {
                if (prefabsEvolutionItems.Count > 0) // если у нас есть  эволюционное оружие, то берем рандомный из списка
                                                     // (с учетом на то, что может быть несколько эволюционных придметов)
                {
                    itemEvolutionFound = true;

                    for (int k = 0; k < prefabsEvolutionItems.Count; k++)
                    {
                        int randomEvolutionObject = Random.Range(0, prefabsEvolutionItems.Count);
                        bool tempBool = true;
                        foreach (GameObject T in gameObjects)
                        {
                            if (T.name == prefabsEvolutionItems[randomEvolutionObject].name)
                            {
                                k = 0;
                                tempBool = false;
                                break;
                            }
                        }

                        if (tempBool == false) continue;


                        // создание кнопки
                        if (prefabsEvolutionItems[randomEvolutionObject].TryGetComponent(out WeaponBase weapon)) //TODO: нужен ли тут if?
                        {

                            GameObject obj = Instantiate(slotPrefab, slotPrefabParent);
                            obj.name = $"LevelUpItem ({i})";
                            slotsList.Add(obj);
                            LevelUpItem levelUpItem = obj.GetComponent<LevelUpItem>();
                            levelUpItem.Init();


                            levelUpItem.SetItemText(weapon.WeaponData.Description);
                            levelUpItem.SetItemImage(weapon.WeaponData.Sprite);


                            foreach (PassiveItem item in InventorySystem.Instance.PassiveItemsList)
                            {
                                if (InventorySystem.Instance.HasPassiveItem(weapon.WeaponData.RequiredPassiveItemToEvolution))
                                {
                                    passiveItemToEvolutionTemp = item;
                                }
                            }



                            levelUpItem.ClearEventItemButton();
                            levelUpItem.AddEventItemButton(() => //TODO: Пофиксить
                            {
                                if (prefabsEvolutionItems.Count > 0)
                                {
                                    EvolutionObject(prefabsEvolutionItems[randomEvolutionObject].GetComponent<WeaponBase>(), passiveItemToEvolutionTemp);
                                }
                            });
                            levelUpItem.AddEventItemButton(() => transform.GetChild(0).gameObject.SetActive(false));
                            levelUpItem.AddEventItemButton(() => ClearSlots());




                            gameObjects.Add(prefabsEvolutionItems[randomEvolutionObject]);
                            prefabsEvolutionItems.Remove(prefabsEvolutionItems[randomEvolutionObject]); // удаление из списка, чтобы добавлять остальные
                            break;
                        }
                    }

                }
                if (itemEvolutionFound == false) // если мы не нашли оружия для эволюции, то берем рандомный объект из списка
                {
                    int randomObject = Random.Range(0, prefabsAllItems.Count);
                    bool tempBool = true;
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
                    levelUpItem.AddEventItemButton(() => ClearSlots());

                    gameObjects.Add(prefabsAllItems[randomObject]);
                    break;
                }
            }
        }




    }


    //public void LevelUp()
    //{
    //    gameObjects = new List<GameObject>(countButon);

    //    int count = 0;
    //    for (int i = 0; i < countButon; i++)
    //    {
    //        for (int j = 0; j < countButon; j++)
    //        {
    //            int randomObject = Random.Range(0, prefabsAllItems.Count);
    //            bool tempBool = true;
    //            count++;
    //            foreach (GameObject T in gameObjects)
    //            {
    //                if (T.name == prefabsAllItems[randomObject].name)
    //                {
    //                    j = 0;
    //                    tempBool = false;
    //                    break;
    //                }
    //            }

    //            if (tempBool == false) continue;

    //            GameObject item = Instantiate(slotPrefab, slotPrefabParent);
    //            item.name = $"LevelUpItem ({i})";
    //            slotsList.Add(item);
    //            LevelUpItem levelUpItem = item.GetComponent<LevelUpItem>();
    //            levelUpItem.Init();

    //            if (prefabsAllItems[randomObject].TryGetComponent(out WeaponBase weapon))
    //            {
    //                levelUpItem.SetItemText(weapon.WeaponData.Description);
    //                levelUpItem.SetItemImage(weapon.WeaponData.Sprite);

    //            }
    //            if (prefabsAllItems[randomObject].TryGetComponent(out PassiveItem passiveItem))
    //            {
    //                levelUpItem.SetItemText(passiveItem.PassiveItemData.Description);
    //                levelUpItem.SetItemImage(passiveItem.PassiveItemData.Sprite);
    //            }

    //            levelUpItem.ClearEventItemButton();
    //            levelUpItem.AddEventItemButton(() => UpgradeObject(prefabsAllItems[randomObject]));
    //            levelUpItem.AddEventItemButton(() => CreateObject(prefabsAllItems[randomObject]));
    //            levelUpItem.AddEventItemButton(() => transform.GetChild(0).gameObject.SetActive(false));

    //            gameObjects.Add(prefabsAllItems[randomObject]);
    //            break;
    //        }
    //    }
    //}
    private void CheckEvolution()
    {
        foreach (var item in InventorySystem.Instance.WeaponsList)
        {
            if (item.WeaponData.CanEvolution)
            {
                InventorySystem.Instance.CanWeaponEvolution(item);
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
    private void EvolutionObject(WeaponBase weapon, PassiveItem requiredPassiveItem)
    {
        EvolutionItemSystem.Instance.EvolutionWeapon(weapon, requiredPassiveItem);
    }
    private void ClearSlots()
    {
        foreach (GameObject item in slotsList)
        {
            Destroy(item);
        }
        slotsList.Clear();
    }
}
