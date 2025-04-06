using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Orb OrbPrefab;
    public Transform GridLayout;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 64; i++)
        {
            Orb newOrb = Instantiate(OrbPrefab, GridLayout);
            newOrb.Element = (Orb.ElementType)Random.Range(0, 6);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
