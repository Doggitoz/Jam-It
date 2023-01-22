using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public GameObject panel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If game state is in UI stuff
        if (GameManager.GM.GetState() != GameState.Progress) return;
        panel.transform.position = Input.mousePosition - Vector3.down * -52f;
    }
}
