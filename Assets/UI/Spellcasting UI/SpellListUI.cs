using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellListUI : MonoBehaviour
{
    public Character Representing;
    public List<SpellWidget> Widgets = new();

    // Start is called before the first frame update
    void Start()
    {
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        for (int i = 0; i < Widgets.Count; i++)
        {
            if (i < Representing.Spells.Count)
            {
                Widgets[i].SetSpell(Representing.Spells[i], Representing == GameManager.Instance.Player);
            }
            else
            {
                Widgets[i].Hide();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
