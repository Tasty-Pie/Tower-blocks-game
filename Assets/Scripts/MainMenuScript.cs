using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] public AudioSource clickSound;

    public void ClickSound()
    {
        clickSound.Play();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public AudioMixer audioMixer;

    public void SetVolume(float volumeLevel)
    {
        //UnityEngine.Debug.Log(VolumeLevel);
        if (volumeLevel <= -9.9)
        {
            audioMixer.SetFloat("mixerVolumeLevel", -80);
        }
        else
        {
            audioMixer.SetFloat("mixerVolumeLevel", volumeLevel);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}