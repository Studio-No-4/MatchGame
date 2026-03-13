using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUI : MonoBehaviour
{
    public ManaType ManaCostType;
    public int CastingCost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Cast()
    {
        if (CanCast())
        {
            GameManager.CurrentCharacter().ManaCollection.Mana[ManaCostType] -= CastingCost;
            GameManager.OpposingCharacter().Health.TakeDamage(1);
        }
    }

    public bool CanCast()
    {
        return GameManager.CurrentCharacter().ManaCollection.Mana[ManaCostType] >= CastingCost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
