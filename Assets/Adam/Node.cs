using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        Mana tempMana = mana;
        mana = null;
        if (!GameManager.CurrentCharacter())
        {
            Object.Destroy(tempMana.gameObject);
            return;
        }

        // Layer 3 is the Overlay Layer
        tempMana.gameObject.layer = 3;
        //mana.TargetPos = Camera.main.ScreenToWorldPoint(GameManager.CurrentCharacter().ManaUI.Sliders[(int)mana.manaType].transform.position);
        tempMana.TargetPos = Camera.main.ScreenToWorldPoint(GameManager.CurrentCharacter().ManaUI.ManaMeters[(int)tempMana.manaType].Counter.transform.position);

        //mana.TargetPos = GameManager.Instance.ManaCollection.position;
        tempMana.SwapSpeed -= Random.Range(0, 50) / 10f;

        GameManager.CurrentCharacter().ManaCollection.AddMana(tempMana.manaType);
        if (tempMana.manaType == ManaType.Skull)
        {
            GameManager.OpposingCharacter().TakeDamage(1);
        }
        if (tempMana.Burning)
        {
            GameManager.CurrentCharacter().TakeDamage(1);
        }
        if (tempMana.Bomb)
        {
            for (int i = Mathf.Max(0, tempMana.xIndex - 1); i < Mathf.Min(8, tempMana.xIndex + 2); i++)
            {
                for (int j = Mathf.Max(0, tempMana.yIndex - 1); j < Mathf.Min(8, tempMana.yIndex + 2); j++)
                {
                    try
                    {
                        if (Board.Instance.manaBoard[i, j].mana) Board.Instance.manaBoard[i, j].PopMana();
                        Debug.Log("BOMBING " + i.ToString() + " " + j.ToString() + " from " + tempMana.xIndex.ToString() + " " + tempMana.yIndex.ToString());
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogWarning(e);
                    }
                }
            }
        }
        
        // Arbitrary 1 second delay currently, should fix later
        Object.Destroy(tempMana.gameObject, 1f);
    }
}
