using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public GameObject panel;

    // Update is called once per frame
    void Update()
    {
        //If game state is in UI stuff
        panel.transform.position = Input.mousePosition - Vector3.down * -52f;
    }
}
