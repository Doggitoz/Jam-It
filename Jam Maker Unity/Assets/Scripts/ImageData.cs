using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageData : MonoBehaviour
{
    public string Name;
    public string Type;
    public string Quip;
    public string Recipe;
    public int TimesMade;

    public GameObject cover;
    public GameObject overlay;
    public GameObject insides;

    public void EnableCover()
    {
        cover.SetActive(true);
    }

    public void DisableOverlay()
    {
        overlay.SetActive(false);
    }

    public void RemoveSubstance()
    {
        //For nothing jam
        insides.SetActive(false);
    }

    public void SetColor(Color color)
    {
        insides.GetComponent<Image>().color = color;
    }

}
