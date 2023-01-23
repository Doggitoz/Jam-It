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
