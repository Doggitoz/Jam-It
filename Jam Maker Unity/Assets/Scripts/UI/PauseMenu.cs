using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider EffectsSlider;
    public Slider UISlider;
    public AudioClip Click;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        SaveData data = GameManager.GM.SaveData;
        MasterSlider.value = data.Data.MasterVolume;
        MusicSlider.value = data.Data.MusicVolume;
        EffectsSlider.value = data.Data.EffectsVolume;
        UISlider.value = data.Data.UIVolume;
    }

    public void ReturnToMainMenu()
    {
        ClickSound();
        CloseUI();
        GameManager.GM.ChangeState(GameState.Menu);
    }

    void CloseUI()
    {
        GameManager.GM.paused = false;
        this.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        ClickSound();
        GameManager.GM.SaveData.SaveToJson();
        Application.Quit();
    }

    public void ResumeGame()
    {
        ClickSound();
        CloseUI();
    }

    public void AbortRecipe()
    {
        ClickSound();
        GameManager.GM.AbortRecipe();
        CloseUI();
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

}
