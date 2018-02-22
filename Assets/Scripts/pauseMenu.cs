using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class pauseMenu : MonoBehaviour {

    public static bool gamePaused = false;

    public GameObject pauseMenuUI;

    public GameObject controll;

    // Use this for initialization
    void Start () {
        pauseMenuUI.SetActive(false);
	}

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void loadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        //load Menu Scene
    }

    public void controls()
    {
        controll.SetActive(true);
        pauseMenuUI.gameObject.SetActive(false);
    }
    public void outOfControl()
    {
        pauseMenuUI.gameObject.SetActive(true);
        controll.SetActive(false);
    }

    public void Restart()   
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quitGame()
    {
        Application.Quit();
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}
}
