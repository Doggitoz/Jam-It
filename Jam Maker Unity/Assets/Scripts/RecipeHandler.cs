using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeHandler : MonoBehaviour
{
    public Ingredient debugOne;
    public Ingredient debugTwo;
    public Ingredient debugThree;

    public Ingredient EmptyIngredients;

    private List<Ingredient> ingredientList = new List<Ingredient>();
    private List<List<Ingredient>> subsets = new List<List<Ingredient>>();

    private Creation currentMadeCreation;
    


    // Start is called before the first frame update
    void Start()
    {
        if (debugOne != null) AddIngredient(debugOne);
        if (debugTwo != null) AddIngredient(debugTwo);
        if (debugThree != null) AddIngredient(debugThree);
        StartRecipeCheck();
    }

    public void RecipeAlgorithm()
    {
        StartRecipeCheck();
    }

    #region Ingredient Manipulation
    public void AddIngredient(Ingredient ing)
    {
        if (ingredientList.Count > 3) return;
        ingredientList.Add(ing);
    }

    public void RemoveIngredient(int slot)
    {
        ingredientList.RemoveAt(slot);
    }

    public void ClearIngredients()
    {
        ingredientList = new List<Ingredient>();
    }
    #endregion

    public List<Ingredient> FillEmpties(List<Ingredient> list)
    {
        while (list.Count < 3)
        {
            list.Add(EmptyIngredients);
        }
        list.Sort();
        return list;
    }

    public void StartRecipeCheck()
    {
        if (ingredientList.Count == 0) return; //Probably handle this elsewhere, but for now here ya go

        //Creates all subsets into subsets<>
        CreateSubsets(new List<Ingredient>(), 0);

        //Remove invalid subsets
        foreach (List<Ingredient> listIng in subsets.ToList())
        {
            if (listIng.Count == 0)
            {
                subsets.Remove(listIng);
                continue;
            }
        }

        //Sort subsets by length first
        subsets.Sort((a, b) => b.Count.CompareTo(a.Count));

        //Populate and sort each subset with empties
        Debug.Log("Subsets----------");
        foreach (List<Ingredient> listIng in subsets)
        {
            List<Ingredient> currList = FillEmpties(listIng);
            currList.Sort((a, b) => a.CompareTo(b));
            Debug.Log(ArrayListToString(listIng));
        }
    }


    public string ArrayListToString(List<Ingredient> printList)
    {
        string str = "";
        foreach (Ingredient ing in printList)
        {
            str += ing.CharacterRepresentation;
        }
        return str;
    }
    public void CreateSubsets(List<Ingredient> output, int currIndex)
    {
        if (currIndex == ingredientList.Count)
        {
            subsets.Add(output);
            return;
        }

        CreateSubsets(new List<Ingredient>(output), currIndex + 1);

        output.Add(ingredientList[currIndex]);
        CreateSubsets(new List<Ingredient>(output), currIndex + 1);
    }
}
