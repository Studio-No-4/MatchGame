using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SmartButton : Button, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    private bool Hovering = false;
    public float x = 150f;

    override public void OnPointerEnter(PointerEventData data)
    {
        base.OnPointerEnter(data);
        Hovering = true;
    }

    override public void OnPointerExit(PointerEventData data)
    {
        base.OnPointerExit(data);
        Hovering = false;
    }


    public void OnPointerMove(PointerEventData data)
    {

    }

    private void Update()
    {
        Quaternion oldRotation = transform.GetChild(0).rotation;
        if (Hovering)
        {
            transform.GetChild(0).LookAt(Input.mousePosition + Vector3.forward * 50f);
        }
        else
        {
            transform.GetChild(0).rotation = Quaternion.identity;
        }
        transform.GetChild(0).rotation = Quaternion.Slerp(oldRotation, transform.GetChild(0).rotation, Time.deltaTime * 50f);
    }
}
