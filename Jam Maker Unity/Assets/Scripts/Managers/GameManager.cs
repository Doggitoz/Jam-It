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
    public bool paused = false;
    public bool ClickedTable;
    public bool ProcessingJam;
    public bool CanMoveOn;
    private Ingredient selectedIngredient;
    [SerializeField] Ingredient[] ingredientList;
    [SerializeField] CreationData _currentCreation;
    public UnityEvent UpdateIngredientSelection { get; private set; }
    public UnityEvent UpdateCreationMade { get; private set; }

    public UnityEvent PlayingStateChangeBroadcast { get; private set; }
    public UnityEvent JuicingIngredient { get; private set; }
    private void Awake()
    {
        CheckManagerInScene();
        if (UpdateIngredientSelection == null)
        {
            UpdateIngredientSelection = new UnityEvent();
        }
        if (UpdateCreationMade == null)
        {
            UpdateCreationMade = new UnityEvent();
        }
        if (PlayingStateChangeBroadcast== null)
        {
            PlayingStateChangeBroadcast = new UnityEvent();
        }
        if (JuicingIngredient == null)
        {
            JuicingIngredient = new UnityEvent();
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
        CanMoveOn = false;
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
            case PlayingState.Finished:
                StartFinished();
                break;
        }
        PlayingStateChangeBroadcast.Invoke();
    }

    public void NextPlayingState()
    {
        if (!CanMoveOn) return;
        if (_currState != GameState.Playing) return;
        CanMoveOn = false;
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
        CanMoveOn = true;
    }

    void StartProcessing()
    {
        SetCamera(22f, 0f);
        //Edge case for crafting nothing
        if (ingredientList[0] == null && ingredientList[1] == null && ingredientList[2] == null)
        {
            CanMoveOn = true;
        }
    }

    void StartMixing()
    {
        CanMoveOn = true;
        _currentCreation = RM.RecipeAlgorithm(ingredientList.ToList());
        if (_currentCreation != null)
        {
            UpdateCreationMade.Invoke();
        }
        SetCamera(44f, 0f);
    }

    void StartHeating()
    {
        CanMoveOn = true;
        SetCamera(66f, 0f);
    }

    void StartFinished()
    {
        SetCamera(88f, 0f);
        Debug.Log(_currentCreation.Name);
        if (_currentCreation != null) _currentCreation.TimesMade++;
        SaveData.SaveToJson();
    }

    public void AbortRecipe()
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
        selectedIngredient = ingredientList[index];

        foreach (Ingredient ing in ingredientList)
        {
            if (ing == null) continue;
            if (ing.IngredientType == IngredientType.Unprocessed)
            {
                return;
            }
        }
        CanMoveOn = true;
        if (type == IngredientType.Smashed)
        {
            SetCamera(22f, 22f);
        }
        else if (type == IngredientType.Juiced)
        {
            SetCamera(22f, 44f);
            JuicingIngredient.Invoke();
        }
    }

    public CreationData GetCurrentCreation()
    {
        return _currentCreation;
    }

    public void EnableMoveOn()
    {
        CanMoveOn = true;
    }

    #endregion

    public void SetCamera(float x, float y)
    {
        Debug.Log("Moving to " + x + " " + y);
        Camera.main.transform.position = new Vector3(x, y, Camera.main.transform.position.z);
    }

}

[System.Serializable]
public enum GameState
{
    Menu, Playing //This might be too many states idrk
}

public enum PlayingState
{
    Picking, Processing, Mixing, Heating, Finished
}