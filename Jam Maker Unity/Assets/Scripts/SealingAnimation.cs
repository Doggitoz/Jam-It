using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealingAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] AudioClip screwOnLid;
    [SerializeField] AudioClip completedJam;
    private void Start()
    {
        GameManager.GM.PlayingStateChangeBroadcast.AddListener(StartAnimation);
    }

    void StartAnimation()
    {
        if (GameManager.GM.GetPlayState() == PlayingState.Finished)
        {
            anim.SetTrigger("CreateJam");
        }
        if (GameManager.GM.GetPlayState() == PlayingState.Picking)
        {
            anim.SetTrigger("Reset");
        }

    }

    void PlaySfx()
    {
        AudioManager.AM.PlayEffect(screwOnLid);
    }

    void AnimationEnd()
    {
        AudioManager.AM.PlayEffect(completedJam);
        GameManager.GM.CanMoveOn = true;
    }
}
