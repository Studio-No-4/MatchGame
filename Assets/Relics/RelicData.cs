using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Relic", menuName = "Content/Relic", order = 0)]
public class RelicData : Item
{
    public UnityEvent OnTurnStart;

    public override void Claim()
    {
        base.Claim();
        Game.Relics.Add(this);
    }

    public void ApplyEffectToSelf(EffectInstance effect)
    {
        GameManager.CurrentCharacter().ApplyEffect(effect);
    }
}
