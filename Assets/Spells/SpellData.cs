using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Spell", menuName = "Content/Spell", order = 0)]
public class SpellData : Item
{
    public List<Cost> Cost;
    public int Shield;

    [Tooltip("True targets the caster, False targets the opponent")]
    public bool SelfTarget = false;
    public EffectInstance Effect;

    public UnityEvent OnCast;

    public override void Claim()
    {
        // Needs options for replacing existing spells
        GameManager.PlayerSpells.Add(this);
    }

    public bool CanCast(Character caster)
    {
        foreach (Cost cost in Cost)
        {
            if (caster.ManaCollection.Mana[cost.Type] < cost.Amount) return false;
        }
        return true;
    }

    public void DealDamage(int Damage = 1)
    {
        GameManager.OpposingCharacter().TakeDamage(GameManager.CurrentCharacter(), Damage);
    }

    public void Cast(Character caster)
    {
        Debug.Log(caster.name + " CASTING SPELL " + name);
        if (CanCast(caster))
        {
            foreach (Cost cost in Cost)
            {
                caster.ManaCollection.Mana[cost.Type] -= cost.Amount;
            }
            if (Effect.Effect)
            {
                if (SelfTarget)
                {
                    GameManager.CurrentCharacter().ApplyEffect(Effect);
                }
                else
                {
                    GameManager.OpposingCharacter().ApplyEffect(Effect);
                }
            }
            OnCast.Invoke();
        }
        else
        {
            Debug.LogWarning("Insufficient Mana");
        }
    }
}

[System.Serializable]
public struct Cost
{
    public ManaType Type;
    public int Amount;

    public Cost(ManaType _type, int _amount = 1)
    {
        Type = _type;
        Amount = _amount;
    }
}