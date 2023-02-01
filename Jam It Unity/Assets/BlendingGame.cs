using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlendingGame : MonoBehaviour
{
    NextKey nextKey = NextKey.W;
    public int fillMax;
    public int fillPerClick;
    int currentFill = 0;
    public Animator anim;
    private bool inFocus = false;
    public TMP_Text textLabel;

    // Start is called before the first frame update
    void Start()
    {
        textLabel.text = "0%";
        GameManager.GM.JuicingIngredient.AddListener(StartMinigame);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inFocus) return;

        if (currentFill > 0)
        {
            anim.SetTrigger("Blend");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (nextKey == NextKey.W)
            {
                currentFill += fillPerClick;
                nextKey = nextKey + 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (nextKey == NextKey.A)
            {
                currentFill += fillPerClick;
                nextKey = nextKey + 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (nextKey == NextKey.S)
            {
                currentFill += fillPerClick;
                nextKey = nextKey + 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (nextKey == NextKey.D)
            {
                currentFill += fillPerClick;
                nextKey = NextKey.W;
            }
        }
        textLabel.text = (Mathf.CeilToInt((float)currentFill / (float)fillMax * 100f)).ToString() + "%";

        if (Mathf.Clamp(currentFill / fillMax, 0f, 1f) == 1)
        {
            CompleteMinigame();
        }

    }

    void StartMinigame()
    {
        inFocus = true;
    }

    void CompleteMinigame()
    {
        GameManager.GM.SetCamera(22f, 0f);
        ResetMinigame();
    }

    void ResetMinigame()
    {
        nextKey = NextKey.W;
        currentFill = 0;
        inFocus = false;
        anim.SetTrigger("Reset");
    }

    private enum NextKey
    {
        W, A, S, D
    }

}
