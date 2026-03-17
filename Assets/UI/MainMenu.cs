using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Transform Settings, Compenduim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Compenduim.gameObject.activeSelf == true)
            {
                Compenduim.gameObject.SetActive(false);
            }
            else if (Settings.gameObject.activeSelf == true)
            {
                Settings.gameObject.SetActive(false);
            }
            
        }
    }

    //create a new game
    public void NewGame()
    {
        //Menu.gameObject.SetActive(false);
    }
    //load a current, not finished game
    public void ContinueGame()
    {

    }
    //Settings
    public void GameSettings()
    {
        Settings.gameObject.SetActive(true);
    }
    //Book of Lore
    public void GameCompedium()
    {
        Compenduim.gameObject.SetActive(true);
    }
    //you know what this does, hopefully
    public void QuitGame()
    {
        //print("QUIT");
        Application.Quit();
    }
}
