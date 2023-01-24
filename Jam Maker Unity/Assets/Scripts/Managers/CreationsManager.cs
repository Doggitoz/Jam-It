using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//The goal of this file is to store an array of all possible creations given a file. Also to store player data and more.
//Should probably use a JSON file for this so its easier to represent all the information in a file.
//Could make storing playerprefs easier too.
//Will likely be reference by the progress UI and the RecipeHandler


public class CreationsManager : MonoBehaviour
{
    [SerializeField] [Tooltip("The order in which it will display in the menu")] List<Creation> allCreations;

    private void Awake()
    {
        Object[] creationObjects = Resources.LoadAll("Creations");

        allCreations = creationObjects.Cast<Creation>().ToList(); //creationObjects.ToList<Creation>();

        allCreations.Sort((x, y) => x.Index.CompareTo(y.Index));

        // I eventually want to save data here using some storing information. Load and save etc for times crafted
        LoadData();
    }

    public void LoadData()
    {
        Debug.Log("TODO: Implement data loading");
    }

    public void UpdateCreation()
    {

    }

    public List<Creation> GetCreationArray()
    {
        return allCreations;
    }

    public Creation GetCreationAtIndex(int index)
    {
        return index < allCreations.Count ? allCreations[index] : null;
    }

    public int GetCreationListSize()
    {
        return allCreations.Count;
    }

}
