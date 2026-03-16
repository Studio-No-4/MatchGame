using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Content/Spell", order = 0)]
public class SpellData : ScriptableObject
{
    public List<Cost> Cost;
    public List<Damage> Damage;
    [TextArea]
    public string Description;
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
[System.Serializable]
public struct Damage
{
    public int Num;

    public Damage(int _amount = 1)
    {
        Num = _amount;
    }
}