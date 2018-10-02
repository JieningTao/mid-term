using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D Thisrigidbody;
    [SerializeField]
    private float AccelrationForce =5;
    [SerializeField]
    private float MaxSpeed = 20;
    [SerializeField]
    private int MaxJumps = 1;


    private float HorizontalInput;
    private int RemainingJumps;
    



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

        
        
        if (Input.GetKeyDown(KeyCode.W)&&RemainingJumps>0)
        {
            RemainingJumps--;
            Thisrigidbody.velocity += new Vector2(0, 10);
        }



 
    }

    private void FixedUpdate()
    {


        Thisrigidbody.AddForce(Vector2.right * HorizontalInput * AccelrationForce);
        Vector2 ClampedVelocity = Thisrigidbody.velocity;
        ClampedVelocity.x = Mathf.Clamp(Thisrigidbody.velocity.x, -MaxSpeed, MaxSpeed);
        Thisrigidbody.velocity = ClampedVelocity;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RemainingJumps = MaxJumps;
    }


}
