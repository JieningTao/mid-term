using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public Rigidbody2D Thisrigidbody;

    [SerializeField]
    private int SerialTest;



	// Use this for initialization
	void Start ()
    {
		




	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKey(KeyCode.D))
        {
            Thisrigidbody.velocity += new Vector2(3, Thisrigidbody.velocity.y);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Thisrigidbody.velocity += new Vector2(-3, Thisrigidbody.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Thisrigidbody.velocity += new Vector2(Thisrigidbody.velocity.x, 3);
        }



        capvelocity();
    }

    
    void capvelocity()
    {
        if (Thisrigidbody.velocity.x < -10)
            Thisrigidbody.velocity = new Vector2(-10,Thisrigidbody.velocity.y);
        if (Thisrigidbody.velocity.x > 10)
            Thisrigidbody.velocity = new Vector2(10, Thisrigidbody.velocity.y);
        if (Thisrigidbody.velocity.y < -10)
            Thisrigidbody.velocity = new Vector2(Thisrigidbody.velocity.x, -10);
        if (Thisrigidbody.velocity.y > 10)
            Thisrigidbody.velocity = new Vector2(Thisrigidbody.velocity.x, 10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }


}
