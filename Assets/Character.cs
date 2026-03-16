using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public ManaCollection ManaCollection = new();
    public ManaCounter ManaUI;
    public Health Health;

    // Start is called before the first frame update
    void Start()
    {

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