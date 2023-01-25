using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject TitleScreen;
    public GameObject OptionsScreen;
    public GameObject CreditsScreen;
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider EffectsSlider;
    public Slider UISlider;
    public AudioClip Click;

    private void Start()
    {
        SaveData data = GameManager.GM.SaveData;
        MasterSlider.value = data.Data.MasterVolume;
        MusicSlider.value = data.Data.MusicVolume;
        EffectsSlider.value = data.Data.EffectsVolume;
        UISlider.value = data.Data.UIVolume;
    }

    public void OpenOptionsMenu()
    {
        ClickSound();
        TitleScreen.SetActive(false);
        OptionsScreen.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        ClickSound();
        TitleScreen.SetActive(true);
        OptionsScreen.SetActive(false);
    }

    public void OpenCreditsMenu()
    {
        ClickSound();
        TitleScreen.SetActive(false);
        CreditsScreen.SetActive(true);
    }

    public void CloseCreditsMenu()
    {
        ClickSound();
        TitleScreen.SetActive(true);
        CreditsScreen.SetActive(false);
    }

    public void StartGame()
    {
        ClickSound();
        GameManager.GM.ChangeState(GameState.Playing);
    }

    public void ExitGame()
    {
        ClickSound();
        GameManager.GM.SaveData.SaveToJson();
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
        AudioManager.AM.SetEffectsVolume(EffectsSlider.value);
    }

    public void UISliderUpdate()
    {
        AudioManager.AM.SetUIVolume(UISlider.value);
    }

    public void ClickSound()
    {
        AudioManager.AM.PlayUI(Click);
    }

    public void ResetDataButton()
    {
        GameManager.GM.SaveData.ClearData();
    }

    public void TestButton()
    {
        CreationData test = GameManager.GM.GetCurrentCreation();
        int index = GameManager.GM.SaveData.Data.CreationData.IndexOf(test);
        GameManager.GM.SaveData.Data.CreationData[index].TimesMade++;
    }
}
