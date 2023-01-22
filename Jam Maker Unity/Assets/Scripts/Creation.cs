using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creation", menuName = "Asset/Creation")]
public class Creation : ScriptableObject
{
    public CreationType Type;
    public string Name;
    public string Recipe;
    public string Quip;
    public bool IsSecret;
    public Color Color;

    public int TimesMade; //If times made > 0, enable color, enable quip, enable recipe
    public int Index;
}

[System.Serializable]
public enum CreationType
{
    Jam, Juice, Error
}
