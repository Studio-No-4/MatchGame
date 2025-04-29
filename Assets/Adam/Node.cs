using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Node
{
    public bool isUsable;

    public Mana mana;

    public Node(bool _isUsable, Mana _mana)
    {
        isUsable = _isUsable;
        mana = _mana;
        //mana.transform.position = transform.position;
        //mana.targetPos = transform.position;
    }

    public void PopMana()
    {
        Object.Destroy(mana.gameObject);
    }
}
