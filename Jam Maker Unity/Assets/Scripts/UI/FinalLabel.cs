using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalLabel : MonoBehaviour
{
    public TMP_Text text;
    private void Start()
    {
        GameManager.GM.PlayingStateChangeBroadcast.AddListener(UpdateLabel);
    }
    void UpdateLabel()
    {
        CreationData cd = GameManager.GM.GetCurrentCreation();
        if (cd == null) return;
        text.text = "Creation: " + cd.Name + " " + cd.Type.ToString();
    }
}
