using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public ProgressUI progressUI;
    public GameObject pauseUI;

    private void Update()
    {
        //Pause menu UI
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.GM.GetState() == GameState.Menu) return;
            //Pause menu logc
            if (GameManager.GM.paused)
            {
                pauseUI.SetActive(false);
                GameManager.GM.paused = false;
                GameManager.GM.SaveData.SaveToJson();
            }
            else
            {
                //Pause
                if (progressUI.isOpen)
                {
                    progressUI.CloseUI();
                }
                pauseUI.SetActive(true);
                GameManager.GM.paused = true;
            }
        }

        //View creations
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameManager.GM.GetState() == GameState.Menu) return;
            if (GameManager.GM.paused) return;
            if (progressUI.isOpen)
            {
                progressUI.CloseUI();
            }
            else
            {
                progressUI.OpenUI();
            }
        }
    }
}
