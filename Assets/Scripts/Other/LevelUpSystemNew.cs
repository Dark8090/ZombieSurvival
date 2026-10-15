using System.Collections.Generic;
using UnityEngine;

public class LevelUpSystemNew : MonoBehaviour
{
    public static LevelUpSystemNew Instance;

    [Header("UI References")]
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private Transform buttonsContainer;
    [SerializeField] private GameObject choiceButtonPrefab;

    [Header("Item Pools")]
    [SerializeField] private List<GameObject> allWeaponPrefabs;
    [SerializeField] private List<GameObject> allPassivePrefabs;

    private void Awake() => Instance = this;

    /// <summary>
    /// Вызывается при повышении уровня игрока
    /// </summary>
    public void ShowLevelUpScreen()
    {
        List<LevelUpChoice> choices = GenerateChoices(3);
        CreateButtons(choices);

        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Генерирует список из N вариантов выбора (Эволюции -> Улучшения/Новые предметы)
    /// </summary>
    private List<LevelUpChoice> GenerateChoices(int count)
    {
        List<LevelUpChoice> choices = new List<LevelUpChoice>();
        List<WeaponBase> inventoryWeapons = InventorySystem.Instance.WeaponsList;
        List<PassiveItem> inventoryPassives = InventorySystem.Instance.PassiveItemsList;

        // ==========================================
        // ЭТАП 1: Поиск доступных эволюций
        // ==========================================
        for (int i = 0; i < inventoryWeapons.Count && choices.Count < count; i++)
        {
            WeaponBase wb = inventoryWeapons[i];
            WeaponData data = wb.WeaponData;

            if (!data.CanEvolution || data.EvolutionWeapon == null || data.RequiredPassiveItemToEvolution == null)
                continue;

            // Проверка макс. уровня оружия
            if (wb.Level < data.MaxLevel)
                continue;

            // Поиск требуемого пассивного предмета в инвентаре
            PassiveItem requiredPassive = null;
            for (int j = 0; j < inventoryPassives.Count; j++)
            {
                PassiveItem pi = inventoryPassives[j];
                if (pi.PassiveItemData == data.RequiredPassiveItemToEvolution &&
                    pi.Level >= pi.PassiveItemData.MaxLevel)
                {
                    requiredPassive = pi;
                    break;
                }
            }

            // Если условия выполнены - добавляем эволюцию
            if (requiredPassive != null)
            {
                LevelUpChoice choice = new LevelUpChoice();
                choice.DisplayData = data.EvolutionWeapon;
                choice.DisplaySprite = data.EvolutionWeapon.Sprite;
                choice.DisplayText = $"{data.EvolutionWeapon.WeaponName}\n✨ ЭВОЛЮЦИЯ!";
                choice.IsEvolution = true;

                // Замыкание захватывает конкретные экземпляры из инвентаря
                choice.OnClickAction = () =>
                {
                    EvolutionItemSystem.Instance.EvolutionWeapon(wb, requiredPassive);
                    CloseLevelUp();
                };

                choices.Add(choice);
            }
        }

        // ==========================================
        // ЭТАП 2: Добиваем улучшениями или новыми предметами
        // ==========================================

        
        while (choices.Count < count)
        {
            LevelUpChoice choice = null;

            // Случайный выбор типа награды (50/50)
            bool offerWeapon = Random.value > 0.5f;

            if (offerWeapon && allWeaponPrefabs.Count > 0)
            {
                GameObject randomPrefab = allWeaponPrefabs[Random.Range(0, allWeaponPrefabs.Count)];

                if (randomPrefab.TryGetComponent(out WeaponBase weaponBase))
                {
                    // Ищем экземпляр этого оружия в инвентаре
                    WeaponBase existingWeapon = null;
                    for (int k = 0; k < inventoryWeapons.Count; k++)
                    {
                        if (inventoryWeapons[k].WeaponData.ID == weaponBase.WeaponData.ID)
                        {
                            existingWeapon = inventoryWeapons[k];
                            break;
                        }
                    }

                    choice = new LevelUpChoice();
                    choice.DisplayData = weaponBase.WeaponData;
                    choice.DisplaySprite = weaponBase.WeaponData.Sprite;

                    if (existingWeapon != null)
                    {
                        // Оружие уже есть -> Улучшение
                        choice.DisplayText = $"{weaponBase.WeaponData.WeaponName}\n(Ур. +1)";
                        choice.IsEvolution = false;

                        choice.OnClickAction = () =>
                        {
                            existingWeapon.Level++;
                            Debug.Log($"Оружие {existingWeapon.WeaponData.WeaponName} улучшено до уровня {existingWeapon.Level}");
                            CloseLevelUp();
                        };
                    }
                    else
                    {
                        // Оружия нет -> Новое оружие
                        choice.DisplayText = $"{weaponBase.WeaponData.WeaponName}\n(Новое!)";
                        choice.IsEvolution = false;

                        choice.OnClickAction = () =>
                        {
                            UIManager.Instance.CreateObject(randomPrefab);
                            CloseLevelUp();
                        };
                    }
                }
            }
            else if (!offerWeapon && allPassivePrefabs.Count > 0)
            {
                GameObject randomPrefab = allPassivePrefabs[Random.Range(0, allPassivePrefabs.Count)];

                if (randomPrefab.TryGetComponent(out PassiveItem passiveComponent))
                {
                    PassiveItemData passiveData = passiveComponent.PassiveItemData;

                    // Ищем экземпляр предмета в инвентаре
                    PassiveItem existingPassive = null;
                    for (int k = 0; k < inventoryPassives.Count; k++)
                    {
                        if (inventoryPassives[k].PassiveItemData.ID == passiveData.ID)
                        {
                            existingPassive = inventoryPassives[k];
                            break;
                        }
                    }

                    choice = new LevelUpChoice();
                    choice.DisplaySprite = passiveData.Sprite;

                    if (existingPassive != null)
                    {
                        // Предмет уже есть -> Улучшение
                        choice.DisplayText = $"{passiveData.Name}\n(Ур. +1)";
                        choice.IsEvolution = false;

                        choice.OnClickAction = () =>
                        {
                            existingPassive.Level++;
                            Debug.Log($"Предмет {passiveData.Name} улучшен до уровня {existingPassive.Level}");
                            CloseLevelUp();
                        };
                    }
                    else
                    {
                        // Предмета нет -> Новый предмет
                        choice.DisplayText = $"{passiveData.Name}\n(Новый!)";
                        choice.IsEvolution = false;

                        choice.OnClickAction = () =>
                        {
                            UIManager.Instance.CreateObject(randomPrefab);
                            CloseLevelUp();
                        };
                    }
                }
            }

            // Добавляем только если успешно создан и не является дубликатом
            if (choice != null)
            {
                bool isDuplicate = false;
                for (int m = 0; m < choices.Count; m++)
                {
                    if (choices[m].DisplayText == choice.DisplayText)
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (!isDuplicate)
                {
                    choices.Add(choice);
                }
            }
        }

        return choices;
    }

    /// <summary>
    /// Создает UI кнопки на основе готового списка выборов
    /// </summary>
    private void CreateButtons(List<LevelUpChoice> choices)
    {
        // Очищаем старые кнопки
        for (int i = buttonsContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(buttonsContainer.GetChild(i).gameObject);
        }

        // Создаем новые кнопки
        for (int i = 0; i < choices.Count; i++)
        {
            LevelUpChoice choice = choices[i];
            GameObject btnGo = Instantiate(choiceButtonPrefab, buttonsContainer);

            LevelUpItem uiItem = btnGo.GetComponent<LevelUpItem>();
            uiItem.SetItemImage(choice.DisplaySprite);
            uiItem.SetItemText(choice.DisplayText);

            // Привязываем действие (оно уже содержит все нужные данные)
            uiItem.ClearEventItemButton();
            uiItem.AddEventItemButton(choice.OnClickAction); //раскоментить
        }
    }

    private void CloseLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}