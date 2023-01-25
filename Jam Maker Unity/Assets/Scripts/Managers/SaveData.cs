using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveData : MonoBehaviour
{
    public Data Data;
    private float timer = 0f;

    private void Awake()
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";
        string data = System.IO.File.ReadAllText(filePath);
        if (data == "" || data == null)
        {
            CreateNewData();
        }
        else
        {
            LoadFromJson();
        }

        Debug.Log("TODO: Eventually allow new creations to not mess up save data");
        Debug.Log("TODO: Make data private for better security. This is bad rn");
    }
    private void Update()
    {
        //Auto saving system
        timer += Time.deltaTime;
        if (timer > 30f)
        {
            timer = 0f;
            SaveToJson();
        }
    }
    #region Data Saving and Loading
    private void CreateNewData()
    {
        Object[] creations = Resources.LoadAll("Creations");
        List<Creation> creationsList = creations.Cast<Creation>().ToList(); //creationObjects.ToList<Creation>();
        creationsList.Sort((x, y) => x.Index.CompareTo(y.Index));
        Data = new Data(creationsList);
        SaveToJson();
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";
        string data = System.IO.File.ReadAllText(filePath);

        Data = JsonUtility.FromJson<Data>(data);

        //Load in volume control
        AudioManager am = AudioManager.AM;
        am.SetMasterVolume(Data.MasterVolume);
        am.SetMusicVolume(Data.MusicVolume);
        am.SetEffectsVolume(Data.EffectsVolume);
        am.SetUIVolume(Data.UIVolume);

        //Load in trigger values

        Debug.Log("Loaded data");
    }

    public void SaveToJson()
    {
        // I want to parse through previous save data to see if Creation id exists, if not, then make new list entry

        string saveData = JsonUtility.ToJson(Data);
        string filePath = Application.persistentDataPath + "/SaveData.json"; //For my local machine: C:/Users/cpWhe/AppData/LocalLow/DefaultCompany/Jam Maker Unity/SaveData.json
        System.IO.File.WriteAllText(filePath, saveData);
        Debug.Log("Saved data");
    }

    public void ClearData()
    {
        CreateNewData();
    }
    #endregion

    #region Creation Data Methods
    public List<CreationData> GetCreationList()
    {
        return Data.CreationData;
    }

    public CreationData GetCreationAtIndex(int index)
    {
        return index < Data.CreationData.Count ? Data.CreationData[index] : null;
    }

    public int GetCreationListCount()
    {
        return Data.CreationData.Count;
    }

    public CreationData GetCreationByRecipe(string recipe, bool hasSmashedIngredient = false)
    {
        foreach (CreationData cr in GetCreationList())
        {
            if (cr.Recipe == recipe)
            {
                if (recipe == "Null") return cr;
                if (recipe == "Door") return cr;
                if (hasSmashedIngredient && cr.Type == CreationType.Jam)
                {
                    Debug.Log("Selected valid recipe: " + cr.Name + " " + cr.Type.ToString());
                    return cr;
                }
                else if (!hasSmashedIngredient && cr.Type == CreationType.Juice)
                {
                    Debug.Log("Selected valid recipe: " + cr.Name + " " + cr.Type.ToString());
                    return cr;
                }
            }
        }
        Debug.Log("No recipe found");
        return null;
    }

    #endregion

}


[System.Serializable]
public class Data
{
    public List<CreationData> CreationData;

    public float MasterVolume;
    public float MusicVolume;
    public float EffectsVolume;
    public float UIVolume;

    public Data(List<Creation> creationList)
    {
        //Create with default values
        CreationData = new List<CreationData>();
        MasterVolume = 1f;
        MusicVolume = 1f;
        EffectsVolume = 1f;
        UIVolume = 1f;

        foreach (Creation cr in creationList)
        {
            CreationData crd = new CreationData();
            crd.CreationID = cr.Id;
            crd.Type = cr.Type;
            crd.Name = cr.Name;
            crd.Recipe = cr.Recipe;
            crd.Quip = cr.Quip;
            crd.IsSecret = cr.IsSecret;
            crd.Color = cr.Color;
            crd.Index = cr.Index;
            crd.TimesMade = 0;

            CreationData.Add(crd);
        }
    }
}

[System.Serializable]
public class CreationData
{
    public int CreationID;
    public CreationType Type;
    public string Name;
    public string Recipe;
    public string Quip;
    public bool IsSecret;
    public Color Color;
    public int Index;
    public int TimesMade;
}