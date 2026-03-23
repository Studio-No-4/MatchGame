using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCostUI : MonoBehaviour
{
    public PipUI PipPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Clear()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }

    public void SetVisuals(List<Cost> data)
    {
        Clear();
        if (data.Count > 0) Show();
        else Hide();
        foreach (Cost cost in data)
        {
            PipUI newPip = Instantiate(PipPrefab, transform);
            newPip.SetVisuals(cost);
        }
    }

    public void SetVisuals(Dictionary<ManaType, int> data)
    {
        Clear();
        if (data.Count > 0) Show();
        else Hide();
        foreach (ManaType mana in data.Keys)
        {
            PipUI newPip = Instantiate(PipPrefab, transform);
            newPip.SetVisuals(mana, data[mana]);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
