using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PasueMenu : MonoBehaviour
{
   [SerializeField] GameObject pauseMenu;

    [Header("---------BUTTON CLICK---------")]
    [SerializeField] AudioSource click;
    [SerializeField] AudioClip buttonClick;


    [Header("--------Change Volume---------")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicSlider;
    private void Start()
    {
        click.clip = buttonClick;
        loadVolume();
    }
    public void PasueGame()
    {
        click.Play();
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = pauseMenu.activeSelf ? 0 : 1;
        musicSlider.gameObject.SetActive(false);
    }

    public void Resume()
    {
        click.Play();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        musicSlider.gameObject.SetActive(false);
    }

    public void Restart()
    {
        Resume();
        int id = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(id);
        PlayerPrefs.DeleteAll();
          
    }

    public void SetupValue()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume" , volume);
    }

    public void Volume()
    {
        musicSlider.gameObject.SetActive(!musicSlider.gameObject.activeSelf);
    }

    void loadVolume()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }

        else
        {
            musicSlider.value = 0.5f;
        }
        SetupValue();
    }
}
