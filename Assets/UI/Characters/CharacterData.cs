using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayableCharacter", menuName = "Content/Character", order = 3)]
public class CharacterData : ScriptableObject
{
    //name of character
    public string Name;

    //image of character
    public Sprite Image;

    //Stats of character
    public int Health;

    //description of character
    [TextArea]
    public string Description;

    //Load the Spell
    public SpellData SpellData;

}
