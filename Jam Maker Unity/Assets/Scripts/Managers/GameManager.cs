using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameState StartingState;
    GameState _currState;
    PlayingState _playingState;
    public RecipeManager RM;
    public SaveData SaveData;

    //Playing state variables
    public bool ClickedTable;
    public bool ProcessingJam;
    [SerializeField] Ingredient[] ingredientList;
    [SerializeField] CreationData _currentCreation;
    public UnityEvent UpdateIngredientSelection { get; private set; }
    private void Awake()
    {
        CheckManagerInScene();
        if (UpdateIngredientSelection == null)
        {
            UpdateIngredientSelection = new UnityEvent();
        }
    }

    private void Start()
    {
        ClickedTable = false;
        ChangeState(StartingState);
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
        Debug.Log("New state: " + newState);
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
            case PlayingState.Finished:
                StartFinished();
                break;
        }

    }

    public void NextPlayingState()
    {
        if (_currState != GameState.Playing) return;
        if (_playingState == PlayingState.Finished)
        {
            ChangePlayingState(PlayingState.Picking);
        }
        else
        {
            ChangePlayingState(_playingState + 1);
        }
    }

    #region Playing Start Methods
    void StartPicking()
    {
        _currentCreation = null; // Reset the crafted creation when entering Picking phase
        ingredientList = new Ingredient[3]; //Clear array
        UpdateIngredientSelection.Invoke();

        if (ClickedTable)
        {
            SetCamera(0f, 11f);
        }
        else
        {
            SetCamera(0f, 0f);
        }
    }

    void StartProcessing()
    {
        //Probably need other UI's above to handle processing stuff

        //Idea: put on starting spot with ingredient. Give option to either juice or smash. After choosing, play minigame
        // once minigame is done, check if more ingredients, if so, restart the StartProcessing() state
        SetCamera(22f, 0f);
    }

    void StartMixing()
    {
        _currentCreation = RM.RecipeAlgorithm(ingredientList.ToList());
        SetCamera(44f, 0f);
    }

    void StartHeating()
    {
        SetCamera(66f, 0f);
    }

    void StartFinished()
    {
        SetCamera(88f, 0f);
        if (_currentCreation != null) _currentCreation.TimesMade++;
        SaveData.SaveToJson();
    }

    void AbortRecipe()
    {
        ChangePlayingState(PlayingState.Picking);
    }

    #endregion

    public void AddIngredient(Ingredient ing)
    {
        for (int i = 0; i < 3; i++)
        {
            if (ingredientList[i] == null)
            {
                ingredientList[i] = ing;
                UpdateIngredientSelection.Invoke();
                return;
            }
        }
    }

    public void RemoveIngredient(int index)
    {
        ingredientList[index] = null; //this might cause a memory leak. :D
        UpdateIngredientSelection.Invoke();
    }

    public Ingredient GetIngredientAtIndex(int index)
    {
        return ingredientList[index];
    }
    public void ProcessIngredient(IngredientType type, int index)
    {
        ingredientList[index].IngredientType = type;
    }

    public CreationData GetCurrentCreation()
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
        Camera.main.transform.position = new Vector3(x, y, Camera.main.transform.position.z);
    }

}

[System.Serializable]
public enum GameState
{
    Menu, Paused, Progress, Playing //This might be too many states idrk
}

public enum PlayingState
{
    Picking, Processing, Mixing, Heating, Finished
}