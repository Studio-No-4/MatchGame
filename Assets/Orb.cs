using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orb : MonoBehaviour
{
    public Image Image;
    public enum ElementType { Water, Fire, Light, Dark, Life, Death }
    public ElementType Element;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateVisuals()
    {
        Image.sprite = GlobalData.Instance.ElementIcons[(int)Element];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisuals();
    }
}
