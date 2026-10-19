using UnityEngine;
using UnityEngine.Events;

// Поле DisplayData удалено: оно нигде не читалось.
public class LevelUpChoice
{
    public Sprite DisplaySprite;
    public string DisplayText;
    public bool IsEvolution;

    public UnityAction OnClickAction;
}