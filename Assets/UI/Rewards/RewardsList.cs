using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsList : MonoBehaviour
{
    public RewardUI RewardPrefab;

    // Should be moved to a different script, needs to be more advanced
    public List<Item> Rewards = new();

    // Start is called before the first frame update
    void Start()
    {
        GenerateRewards();
    }

    /// <summary>
    /// SIMPLIFIED for testing, will improve
    /// </summary>
    public void GenerateRewards()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        for (int i = 0; i < Random.Range(2, 5); i++)
        {
            RewardUI newReward = Instantiate(RewardPrefab, transform);
            newReward.SetItem(Rewards[Random.Range(0, Rewards.Count)]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
