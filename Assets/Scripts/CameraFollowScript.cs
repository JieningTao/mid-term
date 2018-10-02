using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private int viewdistance;


    private Vector2 offest;

    // Use this for initialization
    void Start ()
    {
        offest = transform.position - Player.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3( Player.transform.position.x+offest.x, Player.transform.position.y+offest.y,viewdistance*-1);
        
    }
}
