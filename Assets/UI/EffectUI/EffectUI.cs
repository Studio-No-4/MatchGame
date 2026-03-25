using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectUI : MonoBehaviour
{
    public Image Icon;
    public TMP_Text DurationText;
    public TooltipTrigger Tooltip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateVisuals(EffectInstance effect)
    {
        if (Icon) Icon.sprite = effect.Effect.Icon;
        if (DurationText) DurationText.text = effect.Duration.ToString();
        if (Tooltip) Tooltip.header = effect.Effect.name;
        if (Tooltip) Tooltip.content = effect.Effect.Description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
