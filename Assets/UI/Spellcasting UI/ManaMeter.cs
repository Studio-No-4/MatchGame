using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaMeter : MonoBehaviour
{
    public int Mana = 0;
    public Image Bar;
    public TMP_Text Counter;

    public float Speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Counter.text = Mana.ToString();
        Bar.fillAmount = Mathf.Lerp(Bar.fillAmount, (float)Mana / 10f, Time.deltaTime * Speed);
    }
}
