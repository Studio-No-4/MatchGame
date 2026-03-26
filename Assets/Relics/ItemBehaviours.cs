using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviours : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnBombs(int quantity = 1)
    {
        Board.Instance.SpawnXBombs(quantity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
