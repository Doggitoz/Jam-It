using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageData : MonoBehaviour
{
    public string Name;
    public string Type;
    public string Quip;
    public string Recipe;
    public int TimesMade;

    public GameObject outline;

    public void EnableCover()
    {
        outline.SetActive(true);
    }

}
