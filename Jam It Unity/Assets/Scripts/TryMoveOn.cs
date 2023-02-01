using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryMoveOn : MonoBehaviour
{
    public bool Clickable;
    [SerializeField] AudioClip click;

    public void TriggerMoveOn()
    {
        GameManager.GM.NextPlayingState();
    }

    private void OnMouseDown()
    {
        if (!Clickable) return;
        if (GameManager.GM.CanMoveOn) AudioManager.AM.PlayUI(click);
        TriggerMoveOn();
    }
}
