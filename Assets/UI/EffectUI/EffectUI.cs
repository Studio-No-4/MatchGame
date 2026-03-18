using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectUI : MonoBehaviour
{
    public Image Icon;
    public TMP_Text DurationText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateVisuals(EffectInstance effect)
    {
        if (Icon) Icon.sprite = effect.Effect.Icon;
        if (DurationText) DurationText.text = effect.Duration.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
