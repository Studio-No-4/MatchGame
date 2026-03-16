using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Content/Spell", order = 0)]
public class SpellData : ScriptableObject
{
    public List<Cost> Cost;
    public int Damage;
    public int Shield;
    [TextArea]
    public string Description;

    public bool CanCast(Character caster)
    {
        foreach (Cost cost in Cost)
        {
            if (caster.ManaCollection.Mana[cost.Type] < cost.Amount) return false;
        }
        return true;
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
            GameManager.OpposingCharacter().Health.TakeDamage(Damage);
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