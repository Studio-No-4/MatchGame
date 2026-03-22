using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapNode : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CombatEncounter()
    {
        SceneManager.LoadSceneAsync("DevelopmentCombat");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
