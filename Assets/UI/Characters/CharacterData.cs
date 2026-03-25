using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayableCharacter", menuName = "Content/Character", order = 3)]
public class CharacterData : ScriptableObject
{
    //Image of character
    public Sprite Image;

    //Stats of character
    public int Health;

    //Description of character
    [TextArea] 
    public string Description;

    //Default starting spells
    public List<SpellData> StartingSpells = new();

}
