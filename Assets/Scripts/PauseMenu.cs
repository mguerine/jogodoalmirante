/*
 *  Author: Marcos A. Guerine
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public Button vol_button;
    public Sprite vol_on,vol_mute;
    private bool isMute = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameController.gameInstance.gameIsOver) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
        
    }

    void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameController.gameInstance.GetComponent<AudioSource>().UnPause();
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        GameController.gameInstance.GetComponent<AudioSource>().Pause();
    }

    public void ChangeVolume() {
        isMute = !isMute;
        if (isMute) {
            AudioListener.volume = 0.0f;
            vol_button.GetComponent<Image>().sprite = vol_mute;
        } else {
            AudioListener.volume = 1.0f;
            vol_button.GetComponent<Image>().sprite = vol_on;
        }
        
    }
}
