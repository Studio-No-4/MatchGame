using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadPlayableCharacter : MonoBehaviour
{

    public CharacterData character;
    public TextMeshProUGUI Name, description, stats;
    public Image Image;
    public int Health;


    public void Start()
    {
        LoadCharacter();
    }


    public void Update()
    {
        
    }

    public void LoadCharacter()
    {
        Name.text = character.name;
        Image.sprite = character.Image;
        stats.text = character.Description;
        character.Health = Health;
        Game.PlayerCharacter = character;
        GameManager.PlayerSpells = new List<SpellData>(character.StartingSpells);
    }

}
