using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int HP = 10;
    public int MaxHP = 10;

    public UnityEvent OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
    }

    public virtual void TakeDamage(int damage = 1)
    {
        HP -= damage;
        print(gameObject.ToString() + " took " + damage.ToString() + " damage, now at " + HP.ToString() + " health");
        if (HP <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        OnDeath.Invoke();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
