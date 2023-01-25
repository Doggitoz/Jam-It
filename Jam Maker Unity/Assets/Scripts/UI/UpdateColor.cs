using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateColor : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Image image;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GM.UpdateCreationMade.AddListener(ChangeColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void ChangeColor()
    {
        if (sr != null)
        {
            sr.color = GameManager.GM.GetCurrentCreation().Color;
        }
        if (image != null)
        {
            image.color = GameManager.GM.GetCurrentCreation().Color;
        }
    }
}
