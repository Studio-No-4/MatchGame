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
            GameManager.Instance.PlayerMana[ManaCostType] -= CastingCost;
            GameManager.Instance.Enemy.TakeDamage(1);
        }
    }

    public bool CanCast()
    {
        return GameManager.Instance.PlayerMana[ManaCostType] >= CastingCost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
