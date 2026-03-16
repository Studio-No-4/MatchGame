using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpell : MonoBehaviour
{
    public SpellData Spell;
    public Character Player, Enemy;
    public void SetSpell(SpellData spell)
    {
        Spell = spell;
    }

    void Spelldamage(int Damage)
    {
        Spell.Damage = Damage;
        GameManager.OpposingCharacter().Health.TakeDamage(Damage);
    }

    void Spellshield(int Shield)
    {
        Spell.Shield = Shield;
        GameManager.CurrentCharacter().Health.TakeDamage(Shield);
    }

    //for abilities that get placed on the board
    void Spellactive()
    {

    }
    //for abilities that apply buff 
    void Spellbuff()
    {

    }
    //for abilities that apply debuff 
    void SpellDebuff()
    {

    }
}
