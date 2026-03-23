using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlayableCharacter", menuName = "Content/Character", order = 3)]
public class PlayerableCharacters : ScriptableObject
{
    //name of character
    public string Name;

    //image of character
    public Sprite Image;

    //Stats of character
    public int Health;

    //description of character
    public string Description;

    //Load the Spell
    public SpellData SpellData;

}
