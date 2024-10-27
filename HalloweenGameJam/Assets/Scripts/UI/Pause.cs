using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioSource bgMusic;

    private bool isPaused;
    private float musicVolume;

    private void Start()
    {
        isPaused = false;
        musicVolume = bgMusic.volume; // take original value of the background music's volume
    }

    private void Update()
    {
        // when escape pressed, open pause menu / close pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void Resume()
    {
        pauseMenu.SetActive(false); // close pause menu
        Time.timeScale = 1f; // time will run at its normal pace
        bgMusic.volume = musicVolume; // background music set to original volume
        isPaused = false;
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true); // open pause menu
        Time.timeScale = 0f; // freeze time
        bgMusic.volume = musicVolume / 2; // half background music volume
        isPaused = true;
    }
}
