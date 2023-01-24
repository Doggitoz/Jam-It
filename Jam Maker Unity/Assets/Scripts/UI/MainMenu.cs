using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject TitleScreen;
    public GameObject OptionsScreen;
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider EffectsSlider;
    public Slider UISlider;

    private void Start()
    {
        Debug.Log("TODO: Idrk if I care, but save volume sliders");
    }

    public void OpenOptionsMenu()
    {
        TitleScreen.SetActive(false);
        OptionsScreen.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        TitleScreen.SetActive(true);
        OptionsScreen.SetActive(false);
    }

    public void StartGame()
    {
        GameManager.GM.ChangeState(GameState.Playing);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MasterSliderUpdate()
    {
        AudioManager.AM.SetMasterVolume(MasterSlider.value);
    }
    public void MusicSliderUpdate()
    {
        AudioManager.AM.SetMusicVolume(MusicSlider.value);
    }
    public void EffectsSliderUpdate()
    {
        AudioManager.AM.SetEffctsVolume(EffectsSlider.value);
    }

    public void UISliderUpdate()
    {
        AudioManager.AM.SetUIVolume(UISlider.value);
    }
}
