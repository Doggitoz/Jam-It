using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creation : ScriptableObject
{
    public CreationType Type;
    public string Name;
    public string Recipe;
    public string Quip;
    public int TimesMade; //If times made > 0, change black outline, enable quip, enable recipe
    public bool IsSecret;
    public Color Color;
    public Sprite CreationImage;

    public Creation(string Type, string Name, string Recipe, string Quip, bool IsSecret, Color Color)
    {
        this.Type = TypeFromString(Type);
        this.Name = Name;
        this.Recipe = Recipe; //need to do fancy stuff here probably
        this.Quip = Quip;
        this.IsSecret = IsSecret;
        this.TimesMade = 0; //Handle grabbing the player data somewhere
        this.Color = Color; //Handle taking hex color codes
    }

    public CreationType TypeFromString(string TypeStr)
    {
        CreationType type;

        if (TypeStr.ToLower() == "jam")
        {
            type = CreationType.Jam;
        }
        else if (TypeStr.ToLower() == "juice")
        {
            type = CreationType.Juice;
        }
        else
        {
            type = CreationType.Error;
        }
        return type;
    }
}

[System.Serializable]
public enum CreationType
{
    Jam, Juice, Error
}
