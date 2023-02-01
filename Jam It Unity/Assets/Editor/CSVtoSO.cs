using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVtoSO : MonoBehaviour
{
    private static string creationsCSVPath = "/Editor/CSV/Recipes.csv";



    [MenuItem("Utilities/Generate Creations")]
    public static void GenerateCreations()
    {
        // CSV to SO from https://www.youtube.com/watch?v=1EdLTF43d70
        string[] allLines = File.ReadAllLines(Application.dataPath + creationsCSVPath);

        int index = 1;
        foreach (string line in allLines)
        {
            Debug.Log(line);
            string[] split = line.Split(',');

            Creation creation = ScriptableObject.CreateInstance<Creation>();
            string IDString = TypeFromString(split[0]) + CheckNullName(split[1]);
            int h = 0;
            for (int i = 0; i < IDString.Length; i++)
            {
                h = 31 * h + IDString[i];


                if (h > 1000000000)
                {
                    h = Mathf.FloorToInt(h / 4000f);
                }

            }
            h = Mathf.Abs(h);

            creation.Id = h;
            creation.Type = TypeFromString(split[0]);
            creation.Name = CheckNullName(split[1]);
            creation.Recipe = CheckNullRecipe(split[2]);
            creation.Quip = CheckNullQuip(split[3].Replace("-", ","));
            creation.IsSecret = StringToBool(split[4]);
            creation.Color = HexToRGB(split[5]);
            creation.Index = index;

            AssetDatabase.CreateAsset(creation, $"Assets/Resources/Creations/{creation.Name + creation.Type.ToString()}.asset");
            index++;
        }

        AssetDatabase.SaveAssets();

    }

    private static CreationType TypeFromString(string TypeStr)
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

    private static string CheckNullName(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return "NULL";
        }
        return str;
    }

    private static string CheckNullRecipe(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return "NULL";
        }
        return str;
    }

    private static string CheckNullQuip(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return "";
        }
        return str;
    }

    private static bool StringToBool(string str)
    {
        if (str.ToLower() == "yes")
        {
            return true;
        }
        return false;
    }

    private static Color HexToRGB(string hex)
    {
        if (string.IsNullOrEmpty(hex))
        {
            return Color.white;
        }

        // Code from : https://answers.unity.com/questions/812240/convert-hex-int-to-colorcolor32.html

        hex = hex.Replace("#", ""); //in case the string is formatted #FFFFFF 

        byte a = 255;
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }
}
