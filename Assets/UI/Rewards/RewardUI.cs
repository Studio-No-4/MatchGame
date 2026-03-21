using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
    public Item Reward;
    public TMP_Text ItemName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetItem(Item item)
    {
        Reward = item;
        ItemName.text = item.name;
    }

    public void Claim()
    {
        Reward.Claim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
