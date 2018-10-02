using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    public GameObject Player;
    public int viewdistance;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3( Player.transform.position.x, Player.transform.position.y+2,viewdistance*-1);
        
    }
}
