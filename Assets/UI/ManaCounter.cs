using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaCounter : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> Texts;
    [SerializeField] private List<Image> Sliders;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            Texts[i].text = GameManager.Instance.PlayerMana[(ManaType)i].ToString();
            Sliders[i].fillAmount = 10f / GameManager.Instance.PlayerMana[(ManaType)i];
        }
    }
}
