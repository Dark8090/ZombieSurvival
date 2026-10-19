﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    // Лечение предлагается, когда не осталось ни одного варианта прокачки.
    [Header("Fallback")]
    [SerializeField] private float fallbackHealAmount = 30f;

    private void Awake() => Instance = this;

    public void ShowLevelUpScreen()
    {
        List<LevelUpChoice> choices = GenerateChoices(3);
        CreateButtons(choices);

        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // Цикл while (choices.Count < count) удалён: при нехватке вариантов он зависал навсегда.
    // Теперь собирается конечный список кандидатов, он перемешивается, и из него берутся первые N.
    private List<LevelUpChoice> GenerateChoices(int count)
    {
        List<LevelUpChoice> choices = new List<LevelUpChoice>();

        AddEvolutionChoices(choices, count);

        if (choices.Count < count)
        {
            AddRegularChoices(choices, count);
        }

        // Если вариантов нет совсем, игрок не должен застрять на паузе с пустым экраном.
        if (choices.Count == 0)
        {
            choices.Add(CreateFallbackChoice());
        }

        return choices;
    }

    private void AddEvolutionChoices(List<LevelUpChoice> choices, int count)
    {
        InventorySystem inventory = InventorySystem.Instance;

        foreach (WeaponBase weapon in inventory.WeaponsList)
        {
            if (choices.Count >= count) break;

            // Все условия эволюции (уровни, нужный предмет, сравнение по ID) проверяются в одном месте.
            PassiveItem requiredPassive = inventory.FindEvolutionPassive(weapon);
            if (requiredPassive == null) continue;

            WeaponData evolved = weapon.WeaponData.EvolutionWeapon;
            if (evolved.WeaponPrefab == null || inventory.FindWeapon(evolved.ID) != null) continue;

            WeaponBase weaponToEvolve = weapon;
            choices.Add(CreateChoice(
                evolved.Sprite,
                $"{evolved.WeaponName}\n✨ ЭВОЛЮЦИЯ!",
                true,
                () =>
                {
                    EvolutionItemSystem.Instance.EvolutionWeapon(weaponToEvolve, requiredPassive);
                    CloseLevelUp();
                }));
        }
    }

    private void AddRegularChoices(List<LevelUpChoice> choices, int count)
    {
        InventorySystem inventory = InventorySystem.Instance;
        List<LevelUpChoice> candidates = new List<LevelUpChoice>();

        // Результаты эволюции нельзя получить как обычное новое оружие, только через эволюцию.
        HashSet<int> evolutionResultIds = new HashSet<int>();
        foreach (GameObject prefab in allWeaponPrefabs)
        {
            if (prefab != null && prefab.TryGetComponent(out WeaponBase weaponPrefab) && weaponPrefab.WeaponData.EvolutionWeapon != null)
            {
                evolutionResultIds.Add(weaponPrefab.WeaponData.EvolutionWeapon.ID);
            }
        }

        // Дубликаты отсекаются по ID предмета, а не по тексту кнопки.
        HashSet<int> usedWeaponIds = new HashSet<int>();
        foreach (GameObject prefab in allWeaponPrefabs)
        {
            if (prefab == null || !prefab.TryGetComponent(out WeaponBase weaponPrefab)) continue;

            WeaponData data = weaponPrefab.WeaponData;
            if (!usedWeaponIds.Add(data.ID)) continue;

            WeaponBase existing = inventory.FindWeapon(data.ID);
            GameObject weaponGameObject = prefab;

            if (existing != null)
            {
                // Улучшение предлагается только пока не достигнут MaxLevel.
                if (existing.IsMaxLevel) continue;

                candidates.Add(CreateChoice(
                    data.Sprite,
                    $"{data.WeaponName}\n(Ур. +1)",
                    false,
                    () =>
                    {
                        inventory.LevelUpWeapon(existing);
                        CloseLevelUp();
                    }));
            }
            // Новое оружие предлагается только при наличии свободного слота.
            else if (inventory.HasFreeWeaponSlot && !evolutionResultIds.Contains(data.ID))
            {
                candidates.Add(CreateChoice(
                    data.Sprite,
                    $"{data.WeaponName}\n(Новое!)",
                    false,
                    () =>
                    {
                        UIManager.Instance.CreateObject(weaponGameObject);
                        CloseLevelUp();
                    }));
            }
        }

        HashSet<int> usedPassiveIds = new HashSet<int>();
        foreach (GameObject prefab in allPassivePrefabs)
        {
            if (prefab == null || !prefab.TryGetComponent(out PassiveItem passivePrefab)) continue;

            PassiveItemData data = passivePrefab.PassiveItemData;
            if (!usedPassiveIds.Add(data.ID)) continue;

            PassiveItem existing = inventory.FindPassiveItem(data.ID);
            GameObject passiveGameObject = prefab;

            if (existing != null)
            {
                if (existing.IsMaxLevel) continue;

                // LevelUpPassiveItem сам пересчитывает статы игрока после повышения уровня.
                candidates.Add(CreateChoice(
                    data.Sprite,
                    $"{data.Name}\n(Ур. +1)",
                    false,
                    () =>
                    {
                        inventory.LevelUpPassiveItem(existing);
                        CloseLevelUp();
                    }));
            }
            else if (inventory.HasFreePassiveSlot)
            {
                candidates.Add(CreateChoice(
                    data.Sprite,
                    $"{data.Name}\n(Новый!)",
                    false,
                    () =>
                    {
                        UIManager.Instance.CreateObject(passiveGameObject);
                        CloseLevelUp();
                    }));
            }
        }

        // Перемешивание (Фишер-Йетс), затем добор до нужного количества.
        for (int i = candidates.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (candidates[i], candidates[j]) = (candidates[j], candidates[i]);
        }

        for (int i = 0; i < candidates.Count && choices.Count < count; i++)
        {
            choices.Add(candidates[i]);
        }
    }

    private LevelUpChoice CreateFallbackChoice()
    {
        return CreateChoice(
            null,
            $"Восстановить здоровье\n(+{fallbackHealAmount})",
            false,
            () =>
            {
                GameManager.Instance.PlayerStats.Heal(fallbackHealAmount);
                CloseLevelUp();
            });
    }

    // Общий метод создания варианта: убирает повторяющийся код заполнения LevelUpChoice.
    private static LevelUpChoice CreateChoice(Sprite sprite, string text, bool isEvolution, UnityAction onClick)
    {
        return new LevelUpChoice
        {
            DisplaySprite = sprite,
            DisplayText = text,
            IsEvolution = isEvolution,
            OnClickAction = onClick
        };
    }

    private void CreateButtons(List<LevelUpChoice> choices)
    {
        for (int i = buttonsContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(buttonsContainer.GetChild(i).gameObject);
        }

        for (int i = 0; i < choices.Count; i++)
        {
            LevelUpChoice choice = choices[i];
            GameObject btnGo = Instantiate(choiceButtonPrefab, buttonsContainer);

            LevelUpItem uiItem = btnGo.GetComponent<LevelUpItem>();
            uiItem.SetItemImage(choice.DisplaySprite);
            uiItem.SetItemText(choice.DisplayText);

            uiItem.ClearEventItemButton();
            uiItem.AddEventItemButton(choice.OnClickAction);
        }
    }

    private void CloseLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
