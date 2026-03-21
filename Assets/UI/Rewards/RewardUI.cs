using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
    public Item Reward;

    // Start is called before the first frame update
    void Start()
    {
        
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
