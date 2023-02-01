using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddIngredient : MonoBehaviour
{
    [SerializeField] Ingredient ingredient;
    [SerializeField] AudioClip interact;
    public void AddIngredientToList()
    {
        Ingredient clone = Instantiate(ingredient);
        GameManager.GM.AddIngredient(clone);
    }

    private void OnMouseDown()
    {
        AudioManager.AM.PlayEffect(interact);
        AddIngredientToList();
    }

}
