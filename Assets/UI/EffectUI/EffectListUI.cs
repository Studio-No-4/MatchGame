using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectListUI : MonoBehaviour
{
    public Character Representing;
    public EffectUI EffectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        foreach (EffectInstance instance in Representing.Effects)
        {
            EffectUI newEffectUI = Instantiate(EffectPrefab, transform);
            newEffectUI.UpdateVisuals(instance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
