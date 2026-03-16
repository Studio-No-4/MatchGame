using System.Collections;
using System.Collections.Generic;
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
        //mana.TargetPos = Camera.main.ScreenToWorldPoint(GameManager.CurrentCharacter().ManaUI.Sliders[(int)mana.manaType].transform.position);
        mana.TargetPos = Camera.main.ScreenToWorldPoint(GameManager.CurrentCharacter().ManaUI.ManaMeters[(int)mana.manaType].Counter.transform.position);

        //mana.TargetPos = GameManager.Instance.ManaCollection.position;
        mana.SwapSpeed -= Random.Range(0, 50) / 10f;

        GameManager.CurrentCharacter().ManaCollection.AddMana(mana.manaType);
        if (mana.manaType == ManaType.Skull)
        {
            GameManager.OpposingCharacter().Health.TakeDamage(1);
        }
        
        // Arbitrary 1 second delay currently, should fix later
        Object.Destroy(mana.gameObject, 1f);
        mana = null;
    }
}
