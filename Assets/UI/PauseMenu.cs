using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Transform Menu;
    public static bool Paused = false;

    // Start is called before the first frame update
    void Start()
    {
        Unpause();
    }

    public void TogglePause()
    {
        if (Paused) Unpause();
        else Pause();
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        Paused = false;
        Menu.gameObject.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Paused = true;
        Menu.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
}
