using System;
using UnityEngine;


public class ItemPummarola : PassiveItem
{

    public override float GetRegenerationHealthBonus() => 0.2f * Level;

    //protected override void Start()
    //{
    //    base.Start();
    //}

}

