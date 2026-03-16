using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;

    public Tooltips tooltips;

    public void Awake()
    {
        current = this;

    }

    public void Start()
    {
        Hide();
    }

    public static void Show(string content, string header="")
    {
        current.tooltips.SetText(content, header);
        current.tooltips.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tooltips.gameObject.SetActive(false);

    }
}
