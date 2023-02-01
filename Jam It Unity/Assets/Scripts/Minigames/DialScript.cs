using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialScript : MonoBehaviour
{
    [Range(0, 1)] public float SpinSpeed = .75f;
    public Animator dialAnim;
    public SmashAudio sa;
    public AudioClip success;
    TurningDirection turningDirection = TurningDirection.Left;
    float Location;
    float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < 0.5f) return;
        if (GameManager.GM.GetPlayState() != PlayingState.Processing) return;
        if (turningDirection == TurningDirection.Left)
        {
            Location -= Time.deltaTime * SpinSpeed;
            if (Location < 0f)
            {
                turningDirection = TurningDirection.Right;
            }
        }
        else
        {
            Location += Time.deltaTime * SpinSpeed;
            if (Location > 1f)
            {
                turningDirection = TurningDirection.Left;
            }
        }
        dialAnim.SetFloat("Location", Location);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            timer = 0f;
            if (Location > 0.47 && Location < 0.53)
            {
                AudioManager.AM.PlayEffect(success);
                sa.StartSmash();
            }
        }

    }


    private enum TurningDirection
    {
        Left, Right
    }
}
