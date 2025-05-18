using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = GameManager.Instance.PlayerMana[ManaType.Red].ToString() + " Red Mana";
    }
}
