using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PipUI : MonoBehaviour
{
    public Image Image;
    public TMP_Text Count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetVisuals(Cost cost)
    {
        Image.sprite = GlobalData.Instance.ElementIcons[(int)cost.Type];
        if (cost.Amount > 0)
        {
            Count.text = cost.Amount.ToString();
            Count.gameObject.SetActive(true);
        }
        else
        {
            Count.text = "";
            Count.gameObject.SetActive(false);
        }
    }

    public void SetVisuals(ManaType type, int count=0)
    {
        Image.sprite = GlobalData.Instance.ElementIcons[(int)type];
        if (count > 0)
        {
            Count.text = count.ToString();
            Count.gameObject.SetActive(true);
        }
        else
        {
            Count.text = "";
            Count.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
