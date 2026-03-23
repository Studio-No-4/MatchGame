using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public ManaCollection ManaCollection = new();
    public ManaCounter ManaUI;
    public Health Health;
    public EffectListUI EffectListUI;

    public List<SpellData> Spells = new();

    public List<EffectInstance> Effects = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartTurn()
    {
        foreach (EffectInstance effect in Effects)
        {
            effect.OnStart(this);
        }
    }

    public void EndTurn()
    {
        Board.Instance.ClearBurn();
    }

    public void ApplyEffect(EffectInstance effect)
    {
        for (int i = 0; i < Effects.Count; i++)
        {
            if (Effects[i].Effect == effect.Effect)
            {
                Effects[i] = new(effect.Effect, Effects[i].Duration + effect.Duration);
                EffectListUI.UpdateVisuals();
                return;
            }
        }
        Effects.Add(effect);
        EffectListUI.UpdateVisuals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class ManaCollection
{
    public Dictionary<ManaType, int> Mana = new();

    public ManaCollection()
    {
        Mana[ManaType.Red] = 0;
        Mana[ManaType.Green] = 0;
        Mana[ManaType.Blue] = 0;
        Mana[ManaType.White] = 0;
        Mana[ManaType.Black] = 0;
        Mana[ManaType.Skull] = 0;
    }

    public void AddMana(ManaType type, int value = 1)
    {
        Mana[type] += value;
        if (Mana[type] > 10) Mana[type] = 10;
    }
}