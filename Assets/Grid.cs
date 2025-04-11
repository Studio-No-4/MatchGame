using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Orb OrbPrefab;
    public Transform GridLayout;
    public List<Orb> OrbElements = new();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 64; i++)
        {
            Orb newOrb = Instantiate(OrbPrefab, GridLayout);
            newOrb.Element = (Orb.ElementType)Random.Range(0, 6);
            OrbElements.Add(newOrb);
        }
        checkMatch();
    }

    void checkMatch()
    {
        for (int i = 0; i < OrbElements.Count-2; i++) { 
        
            if (OrbElements[i].Element == OrbElements[i + 1].Element && OrbElements[i].Element == OrbElements[i + 2].Element)
            {
                Destroy(OrbElements[i].gameObject);
                Destroy(OrbElements[i + 1].gameObject);
                Destroy(OrbElements[i + 2].gameObject);
            }
        }
        for (int i = 0; i < OrbElements.Count - 16; i++) {
            if (OrbElements[i].Element == OrbElements[i + 8].Element && OrbElements[i].Element == OrbElements[i + 16].Element)
            {
                Destroy(OrbElements[i].gameObject);
                Destroy(OrbElements[i + 8].gameObject);
                Destroy(OrbElements[i + 16].gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
