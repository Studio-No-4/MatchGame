using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicListUI : MonoBehaviour
{
    public RelicUI RelicUIPrefab;

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
        foreach (RelicData relic in Game.Relics)
        {
            RelicUI newRelic = Instantiate(RelicUIPrefab, transform);
            newRelic.SetRelic(relic);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
