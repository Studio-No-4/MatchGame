using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectScreen : MonoBehaviour
{
    public CharacterData DefaultCharacter;
    public TMP_Text CharacterName;
    public TMP_Text CharacterDescription;
    public SpellWidget SpellWidget;
    public TMP_Text SpellDescription;
    public Image Image;

    // Start is called before the first frame update
    void Start()
    {
        SelectCharacter(DefaultCharacter);
    }

    public void SelectCharacter(CharacterData character)
    {
        Game.PlayerCharacter = character;
        CharacterName.text = character.name;
        CharacterDescription.text = character.Description;
        Image.sprite = character.Image;
        SpellWidget.SetSpell(character.StartingSpells[0]);
        SpellDescription.text = character.StartingSpells[0].Description;
        GameManager.PlayerSpells = new List<SpellData>(character.StartingSpells);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
