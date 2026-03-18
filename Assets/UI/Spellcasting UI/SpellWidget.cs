using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellWidget : MonoBehaviour
{
    public SpellListUI List;
    public SpellData Spell;
    public TMP_Text Title;
    public Button Button;
    public TooltipTrigger Tooltip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetSpell(SpellData spell, bool interactable = true)
    {
        Spell = spell;
        Button.interactable = true;
        Button.enabled = interactable;
        Title.text = spell.name;
        Tooltip.content = spell.Description;
        Tooltip.enabled = true;
    }

    public void Hide()
    {
        Spell = null;
        Button.interactable = false;
        Title.text = "";
        Tooltip.enabled = false;
    }

    public void Cast()
    {
        Spell.Cast(List.Representing);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
