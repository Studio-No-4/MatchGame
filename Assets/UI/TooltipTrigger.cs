using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    //private static LTDescr delay;  <= suppose to add a delay
    public string header;

    [Multiline()]
    public string content;
    

    public void  OnPointerEnter(PointerEventData eventData)
    {

        //delay = LeanTween.delayCall(0.5f, () => 
        //{ 
        TooltipSystem.Show(content, header);
        //});   <== Delay function for tooltips, not working
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }

}
