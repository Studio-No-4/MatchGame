using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic", menuName = "Content/Relic", order = 0)]
public class RelicData : Item
{
    public override void Claim()
    {
        base.Claim();
        Game.Relics.Add(this);
    }
}
