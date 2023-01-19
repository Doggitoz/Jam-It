using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, System.IComparable<Ingredient>
{
    public string CharacterRepresentation;
    public string IngredientName;
    public IngredientType IngredientType = IngredientType.Unprocessed;

    public int CompareTo(Ingredient other)
    {
        return string.Compare(CharacterRepresentation, other.CharacterRepresentation);
    }

}

public enum IngredientType
{
    Unprocessed, Smashed, Juiced
}