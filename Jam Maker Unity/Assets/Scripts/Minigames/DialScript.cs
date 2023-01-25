using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialScript : MonoBehaviour
{
    [Range(0, 1)] public float SpinSpeed = 1f;
    public Animator anim;
    TurningDirection turningDirection = TurningDirection.Left;
    float Location;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        anim.SetFloat("Location", Location);
    }


    private enum TurningDirection
    {
        Left, Right
    }
}
