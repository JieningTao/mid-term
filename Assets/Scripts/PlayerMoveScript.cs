using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D Thisrigidbody;
    [SerializeField]
    private float speed =5;

    private float HorizontalInput;



	// Use this for initialization
	void Start ()
    {

      



	}
	
	// Update is called once per frame
	void Update ()
    {
        HorizontalInput = Input.GetAxis("Horizontal");


        /*
        if (Input.GetKey(KeyCode.D))
        {
            Thisrigidbody.AddForce(new Vector2(3, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            Thisrigidbody.AddForce(new Vector2(-3, 0));
        }
        */

        
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            Thisrigidbody.velocity += new Vector2(0, 5);
        }



        //capvelocity();
    }

    private void FixedUpdate()
    {


        Thisrigidbody.AddForce(Vector2.right * HorizontalInput * speed);



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
