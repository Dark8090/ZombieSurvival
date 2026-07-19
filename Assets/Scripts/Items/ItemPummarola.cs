using System;
using UnityEngine;


public class ItemPummarola : PassiveItem
{
    [Range(1, 3)] public int LevelUpgrade = 1;

    public override float GetRegenerationHealthBonus() => 0.2f * LevelUpgrade;


}

