using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour {
    public Rigidbody2D Thisrigidbody;

	// Use this for initialization
	void Start () {
		




	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.D))
        {
            Thisrigidbody.velocity = new Vector2(1, Thisrigidbody.velocity.y);
        }
        


    }
}
