using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddIngredient : MonoBehaviour
{
    [SerializeField] Ingredient ingredient;
    public void AddIngredientToList()
    {
        Ingredient clone = Instantiate(ingredient);
        GameManager.GM.AddIngredient(clone);
    }

    private void OnMouseDown()
    {
        AddIngredientToList();
    }

}
