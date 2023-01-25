using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{

    List<List<Ingredient>> subsets = new List<List<Ingredient>>();

    bool hasSmashedIngredient = false;
    
    public CreationData RecipeAlgorithm(List<Ingredient> ingredientList)
    {
        subsets = new List<List<Ingredient>>();

        foreach (Ingredient ing in ingredientList.ToList())
        {
            if (ing == null)
            {
                ingredientList.Remove(ing);
            }
        }

        //Check if empty
        if (ingredientList.Count == 0)
            return GameManager.GM.SaveData.GetCreationByRecipe("Null");

        ingredientList.Sort((x, y) => x.CharacterRepresentation.CompareTo(y.CharacterRepresentation));

        //Check if door
        foreach (Ingredient ing in ingredientList.ToList())
        {
            if (ing.IngredientName == "Table")
            {
                GameManager.GM.ClickedTable = true;
                return GameManager.GM.SaveData.GetCreationByRecipe("Door");
            }
        }

        //Check if there is a smashed ingredient
        foreach (Ingredient ing in ingredientList.ToList())
        {
            if (ing.IngredientType == IngredientType.Smashed)
            {
                hasSmashedIngredient = true;
                break;
            }
        }

        //Creates all subsets into subsets<>
        CreateSubsets(ingredientList, new List<Ingredient>(), 0);

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

        //Filter all subsets into size lists
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
                    Application.Quit();
                    return null;
            }
        }
        CreationData creation;
        subsetSizeTwo = subsetSizeTwo.OrderBy(_ => Random.Range(0, 100f)).ToList();
        subsetSizeOne = subsetSizeOne.OrderBy(_ => Random.Range(0, 100f)).ToList();

        //If i can randomize order of subset size two and one, this works
        foreach (List<Ingredient> ing in subsetSizeThree)
        {
            creation = GameManager.GM.SaveData.GetCreationByRecipe(ArrayListToString(ing), hasSmashedIngredient);
            if (creation != null)
                return creation;
        }
        foreach (List<Ingredient> ing in subsetSizeTwo)
        {
            creation = GameManager.GM.SaveData.GetCreationByRecipe(ArrayListToString(ing), hasSmashedIngredient);
            if (creation != null)
                return creation;
        }
        foreach (List<Ingredient> ing in subsetSizeOne)
        {
            creation = GameManager.GM.SaveData.GetCreationByRecipe(ArrayListToString(ing), hasSmashedIngredient);
            if (creation != null)
                return creation;
        }

        Application.Quit();
        Debug.LogWarning("Somehow it didn't find a creation");
        return null;
    }
    public string ArrayListToString(List<Ingredient> printList)
    {
        string str = "";
        foreach (Ingredient ing in printList.ToList())
        {
            str += ing.CharacterRepresentation;
        }
        return str;
    }
    public void CreateSubsets(List<Ingredient> ingredientList, List<Ingredient> output, int currIndex)
    {
        if (currIndex == ingredientList.Count)
        {
            subsets.Add(output);
            return;
        }

        CreateSubsets(ingredientList, new List<Ingredient>(output), currIndex + 1);

        output.Add(ingredientList[currIndex]);
        CreateSubsets(ingredientList, new List<Ingredient>(output), currIndex + 1);
    }
}
