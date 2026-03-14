using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaCounter : MonoBehaviour
{
    public Character Representing;
    [SerializeField] public List<TMP_Text> Texts;
    public List<Image> Sliders;

    [SerializeField] private float Speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            Texts[i].text = Representing.ManaCollection.Mana[(ManaType)i].ToString();
            Sliders[i].fillAmount = (float)Representing.ManaCollection.Mana[(ManaType)i] / 10f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            Texts[i].text = Representing.ManaCollection.Mana[(ManaType)i].ToString();
            Sliders[i].fillAmount = Mathf.Lerp(Sliders[i].fillAmount, (float)Representing.ManaCollection.Mana[(ManaType)i] / 10f, Time.deltaTime * Speed);
        }
    }
}
