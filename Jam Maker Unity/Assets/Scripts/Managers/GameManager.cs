using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameState StartingState;
    GameState _currState;
    PlayingState _playingState;
    public RecipeManager RM;
    public CreationsManager CM;

    //Playing state variables
    public bool ClickedTable;
    public bool ProcessingJam;
    List<Ingredient> ingredientList = new List<Ingredient>(3);
    Creation _currentCreation;
    private void Awake()
    {
        CheckManagerInScene();
    }

    private void Start()
    {
        ChangeState(StartingState);
        Debug.Log("TODO: Implement door and table logic (including saving to files)");
        ClickedTable = false; //CHECK THIS IN JSON FILES LATER
    }
    #region GameManager Singleton
    static private GameManager gm;
    static public GameManager GM { get { return gm; } }

    void CheckManagerInScene()
    {

        if (gm == null)
        {
            gm = this; 
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region GameState Management

    public GameState GetState() { return _currState; }

    public void ChangeState(GameState newState)
    {
        _currState = newState;

        switch(_currState)
        {
            case GameState.Menu:
                StartMenuState();
                break;
            case GameState.Progress:
                StartProgressState();
                break;
            case GameState.Paused:
                StartPausedState();
                break;
            case GameState.Playing:
                StartPlayingState();
                break;
            //etc etc
        }

    }
    #endregion

    #region Menu State

    void StartMenuState()
    {
        SceneManager.LoadScene(0);
    }

    void UpdateMenuState()
    {

    }

    #endregion

    #region Playing State

    void StartPlayingState()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
            SceneManager.LoadScene(1);
        ChangePlayingState(PlayingState.Picking);
    }

    void UpdatePlayingState()
    {

    }

    public PlayingState GetPlayState() { return _playingState; }

    public void ChangePlayingState(PlayingState newState)
    {
        _playingState = newState;

        switch (_playingState)
        {
            case PlayingState.Picking:
                StartPicking();
                break;
            case PlayingState.Processing:
                StartProcessing();
                break;
            case PlayingState.Mixing:
                StartMixing();
                break;
            case PlayingState.Heating:
                StartHeating();
                break;
            case PlayingState.Packaging:
                StartPackaging();
                break;
            case PlayingState.Finished:
                StartFinished();
                break;
        }

    }

    public void NextPlayingState()
    {
        if (_playingState == PlayingState.Finished)
        {
            ChangePlayingState(PlayingState.Picking);
        }
        else
        {
            ChangePlayingState(_playingState++);
        }
    }

    #region Playing Start Methods
    void StartPicking()
    {
        _currentCreation = null; // Reset the crafted creation when entering Picking phase
        ingredientList.Clear();

        //Have logic to check if door is missing or not?
        SetCamera(0f, 1f);
    }

    void StartProcessing()
    {
        //Probably need other UI's above to handle processing stuff

        //Idea: put on starting spot with ingredient. Give option to either juice or smash. After choosing, play minigame
        // once minigame is done, check if more ingredients, if so, restart the StartProcessing() state

    }

    void StartMixing()
    {
        _currentCreation = RM.RecipeAlgorithm(ingredientList);
        SetCamera(46f, 1f);
    }

    void StartHeating()
    {
        SetCamera(69f, 1f);
    }

    void StartPackaging()
    {
        SetCamera(92f, 1f);
    }

    void StartFinished()
    {
        SetCamera(115f, 1f);
    }

    #endregion

    public void AddIngredient(Ingredient ing)
    {
        for (int i = 0; i < 3; i++)
        {
            if (ingredientList[i] != null)
            {
                ingredientList.Insert(i, ing);
                Debug.Log("Inserted " + ing.IngredientName + " to slot " + i);
            }
        }
    }

    public  void RemoveIngredient(int index)
    {
        ingredientList.RemoveAt(index);
    }

    public Creation GetCurrentCreation()
    {
        return _currentCreation;
    }

    #endregion

    #region Progress State

    void StartProgressState()
    {

    }

    void UpdateProgressState()
    {

    }

    #endregion

    #region Paused State

    void StartPausedState()
    {

    }

    void UpdatePausedState()
    {

    }

    #endregion

    

    public void SetCamera(float x, float y)
    {
        Camera.main.transform.position = new Vector3(x, y, -10f);
    }

}

[System.Serializable]
public enum GameState
{
    Menu, Paused, Progress, Playing //This might be too many states idrk
}

public enum PlayingState
{
    Picking, Processing, Mixing, Heating, Packaging, Finished
}