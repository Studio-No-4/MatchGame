using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellBook : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalscale;
    public Vector3 pressedScale = new Vector3(0.9f, 0.9f, 0.9f);

    void Awake()
    {
        originalscale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = pressedScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = originalscale;
    }
}
