using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Asset/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string CharacterRepresentation;
    public string IngredientName;
    public IngredientType IngredientType = IngredientType.Unprocessed;
    public Sprite Sprite;
    public Color color;
}

public enum IngredientType
{
    Unprocessed, Smashed, Juiced
}