using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Content/Enemy", order = 4)]
public class EnemyData : ScriptableObject
{
    //Image of Enemy
    public Sprite Image;

    //Stats of Enemy
    public int Health;

    //Description of Enemy
    [TextArea]
    public string Description;

    //Default starting spells
    public List<SpellData> StartingSpells = new();
}
