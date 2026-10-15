using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelUpChoice
{
    public WeaponData DisplayData;      // Что показываем на кнопке (может быть эволюционным оружием)
    public Sprite DisplaySprite;        // Иконка для кнопки
    public string DisplayText;          // Текст для кнопки
    public bool IsEvolution;            // Флаг для визуального выделения

    // Действие при нажатии (замыкание создается ОДИН раз с правильными данными)
    public UnityAction OnClickAction;
}
