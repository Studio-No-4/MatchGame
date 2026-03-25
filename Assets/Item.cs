using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public Sprite Icon;
    [Multiline]
    public string Description;

    public virtual void Claim()
    {

    }
}
