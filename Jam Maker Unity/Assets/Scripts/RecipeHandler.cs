using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeHandler : MonoBehaviour
{
    public Ingredient debugOne;
    public Ingredient debugTwo;
    public Ingredient debugThree;

    List<Ingredient> ingredientList = new List<Ingredient>();
    List<List<Ingredient>> subsets = new List<List<Ingredient>>();

    Creation currentMadeCreation;
    bool hasSmashedIngredient = false;
    [SerializeField] CreationsHandler ch;
    


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TODO: Rework algorithm to accomodate empties and jam vs juice");
        if (debugOne != null) AddIngredient(debugOne);
        if (debugTwo != null) AddIngredient(debugTwo);
        if (debugThree != null) AddIngredient(debugThree);
        if (ch == null) Debug.LogError("Fatal error: CreationsHandler not set on Recipe Handler");
        StartRecipeCheck();
    }

    public void RecipeAlgorithm()
    {
        ClearIngredients();
        subsets = new List<List<Ingredient>>();
        if (ingredientList.Count == 0) return; //Probably handle this elsewhere, but for now here ya go
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
        currentMadeCreation = null;
        hasSmashedIngredient = false;
    }
    #endregion

    public void StartRecipeCheck()
    {
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

        List<List<Ingredient>> subsetSizeThree = new List<List<Ingredient>>();
        List<List<Ingredient>> subsetSizeTwo = new List<List<Ingredient>>();
        List<List<Ingredient>> subsetSizeOne = new List<List<Ingredient>>();


        //Sort each subset and fill them with empties
        foreach (List<Ingredient> listIng in subsets)
        {
            switch (listIng.Count)
            {
                case 3:
                    subsetSizeThree.Add(listIng);
                    break;
                case 2:
                    subsetSizeTwo.Add(listIng);
                    break;
                case 1:
                    subsetSizeOne.Add(listIng);
                    break;
                default:
                    Debug.LogError("Fatal Error: Somehow a subset was empty or bigger than 3");
                    return;
            }
        }

        foreach (Ingredient ing in ingredientList)
        {
            if (ing.IngredientType == IngredientType.Smashed)
            {
                hasSmashedIngredient = true;
                break;
            }
        }

        //If there is three ingredients, check if it has a valid recipe
        if (subsetSizeThree.Count > 0)
        {
            if (FindValidRecipe(subsetSizeThree[0]))
            {
                return;
            }
        }
        //Check all valid subsets of two ingredients for valid recipes
        while (subsetSizeTwo.Count > 0)
        {
            int randomIndex = Random.Range(0, subsetSizeTwo.Count);
            if (FindValidRecipe(subsetSizeTwo[randomIndex]))
            {
                return;
            }
            else subsetSizeTwo.RemoveAt(randomIndex);
        }
        while (subsetSizeOne.Count > 0)
        {
            int randomIndex = Random.Range(0, subsetSizeOne.Count);
            if (FindValidRecipe(subsetSizeOne[randomIndex]))
            {
                return;
            }
            else subsetSizeOne.RemoveAt(randomIndex);
        }
    }

    public bool FindValidRecipe(List<Ingredient> recipe)
    {
        //foreach (Creation cr in ch.GetCreationArray())
        //{
        //    if (cr.Recipe == ArrayListToString(recipe))
        //    {
        //        if (hasCrushedIngredient && cr.Type == CreationType.Jam)
        //        {
        //            Debug.Log("Found valid recipe: " + cr.Name + " " + cr.Type.ToString());
        //            return true;
        //        }
        //        else if (!hasCrushedIngredient && cr.Type == CreationType.Juice)
        //        {
        //            Debug.Log("Found valid recipe: " + cr.Name + " " + cr.Type.ToString());
        //            return true;
        //        }
        //    }

        //}
        return false;
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
