using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SystemGame : MonoBehaviour
{
    bool paused;
    public GameObject framePause;
    public string Level;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
        QuitGame();
       
    }

    void Pause()
    {
        Time.timeScale = 0;
        framePause.SetActive(true);
    }

    void UnPause()
    {
        Time.timeScale = 1;
        framePause.SetActive(false);
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if (paused)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Loadnivel()
    {
        Application.LoadLevel(Level);
        Time.timeScale = 1;
    }
}
