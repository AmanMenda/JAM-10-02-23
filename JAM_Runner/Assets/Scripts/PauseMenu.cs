using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu pauser;
    public bool GameIsPaused;
    public bool click, change;
    public GameObject pauseMenuUI;

    void Awake()
    {
        pauser = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (click && PlayerController.player_control.death == false) {
            Pause();
            change = true;
        } else if (PlayerController.player_control.death == true && click == true)  {
            Death();
        } else {
            Resume();
        }
    }

    public void pause_is_cliqued() 
    {
        click = true;
    }

    public void Resume()
    {
        click = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Pause()
    {
        click = true;
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }

    void Death()
    {
        click = true;

        Time.timeScale = 0;
    } 
}
