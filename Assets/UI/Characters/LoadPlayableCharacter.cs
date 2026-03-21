using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoadPlayableCharacter : MonoBehaviour
{

    public PlayerableCharacters character;
    public void LoadName(string name)
    {
        character.name = name;
    }

    public void LoadImage(Sprite image)
    {
        character.Image = image;
    }

    public void LoadDescription(string description)
    {
        character.Description = description;
    }

    public void LoadHealth(int health)
    {
        character.Health = health;
    }

    //public void LoadSpell(string character)
    //{
    //Character = character;
    //}
}
