using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaCounter : MonoBehaviour
{
    public Character Representing;
    public List<ManaMeter> ManaMeters = new();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            ManaMeters[i].Mana = Representing.ManaCollection.Mana[(ManaType)i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            ManaMeters[i].Mana = Representing.ManaCollection.Mana[(ManaType)i];
        }
    }
}
