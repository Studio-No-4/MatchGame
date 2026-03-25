using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicUI : MonoBehaviour
{
    public Image Icon;
    public TooltipTrigger TooltipData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetRelic(RelicData relic)
    {
        Icon.sprite = relic.Icon;
        TooltipData.header = relic.name;
        TooltipData.content = relic.Description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
