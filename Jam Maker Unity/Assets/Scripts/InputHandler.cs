using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public ProgressUI progressUI;
    public PauseMenu pauseUI;


    private void Update()
    {
        //Pause menu UI
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Pause menu logc
            if (GameManager.GM.GetState() == GameState.Paused)
            {
                //Unpause
            }
            else
            {
                //Pause
            }
        }

        //View creations
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameManager.GM.GetState() == GameState.Menu) return;
            if (GameManager.GM.GetState() == GameState.Paused) return;

            if (progressUI.isOpen)
            {
                progressUI.CloseUI();
                GameManager.GM.ChangeState(GameState.Playing);
            }
            else
            {
                progressUI.OpenUI();
                GameManager.GM.ChangeState(GameState.Progress);
            }
        }

    }
}
