using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
     
    }

    public void restartAfterWin()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void quitGame()
    {
        Application.Quit();
    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
