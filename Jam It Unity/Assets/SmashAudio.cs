using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAudio : MonoBehaviour
{
    public Animator anim;
    private float timer = 0f;
    private bool hasSmashed = false;
    public AudioClip smashSound;
    private void Update()
    {
        if (hasSmashed) timer += Time.deltaTime;
        if (timer > 2f) CompleteScene();
    }

    public void StartSmash()
    {
        timer = 0f;
        anim.SetTrigger("Smash");
    }

    void EndSmash()
    {
        AudioManager.AM.PlayEffect(smashSound);
        hasSmashed = true;
    }

    void CompleteScene()
    {
        timer = 0f;
        anim.SetTrigger("Reset");
        hasSmashed = false;
        GameManager.GM.SetCamera(22f, 0f);
    }
}
