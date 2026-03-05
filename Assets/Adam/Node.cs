using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    }

    public void PopMana()
    {
        mana.TargetPos = Camera.main.ScreenToWorldPoint(ManaCounter.Instance.Sliders[(int)mana.manaType].transform.position);
        
        //mana.TargetPos = GameManager.Instance.ManaCollection.position;
        mana.SwapSpeed -= Random.Range(0, 50) / 10f;
        if (GameManager.Instance.PlayerMana.ContainsKey(mana.manaType))
        {
            GameManager.Instance.PlayerMana[mana.manaType] += 1;
            if (GameManager.Instance.PlayerMana[mana.manaType] > 10) GameManager.Instance.PlayerMana[mana.manaType] = 10;
        }
        else
        {
            GameManager.Instance.PlayerMana[mana.manaType] = 1;
        }
        // Arbitrary 1 second delay currently, should fix later
        Object.Destroy(mana.gameObject, 1f);
        mana = null;
    }
}
