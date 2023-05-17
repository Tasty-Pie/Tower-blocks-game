using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public AudioMixer audioMixer;

    public void SetVolume(float volumeLevel)
    {
        //UnityEngine.Debug.Log(VolumeLevel);
        audioMixer.SetFloat("mixerVolumeLevel", volumeLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}