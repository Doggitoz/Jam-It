using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameState currState;

    #region GameManager Singleton
    static private GameManager gm; //refence GameManager
    static public GameManager GM { get { return gm; } } //public access to read only gm 

    //Check to make sure only one gm of the GameManager is in the scene
    void CheckGameManagerIsInScene()
    {

        //Check if instnace is null
        if (gm == null)
        {
            gm = this; //set gm to this gm of the game object
            Debug.Log(gm + " Loaded");
        }
        else //else if gm is not null a Game Manager must already exsist
        {
            Destroy(this.gameObject); //In this case you need to delete this gm
            Debug.Log("Game Manager exists. Deleting...");
        }
    }//end CheckGameManagerIsInScene()
    #endregion

    private void Awake()
    {
        CheckGameManagerIsInScene();
    }

    private void Start()
    {
        
    }

    public GameState GetState() { return currState; }

    public void ChangeState(GameState newState)
    {
        currState = newState;

        switch(currState)
        {
            case GameState.Menu:
                break;
            //etc etc
        }

    }

}

[System.Serializable]
public enum GameState
{
    Menu, Paused, Progress, Playing //This might be too many states idrk
}