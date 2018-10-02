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
    private float JumpForce = 15;
    [SerializeField]
    private float MaxSpeed = 20;
    [SerializeField]
    private int MaxJumps = 1;
    [SerializeField]
    private ContactFilter2D GroundContactFilter;
    [SerializeField]
    private Collider2D GroundDetectTrigger;
    [SerializeField]
    private ContactFilter2D WallContactFilter;
    [SerializeField]
    private Collider2D WallDetectTrigger;


    private float HorizontalInput;
    private int RemainingJumps;
    private Collider2D[] GroundHitResults = new Collider2D[16];
    private Collider2D[] WallHitResults = new Collider2D[16];

    // Use this for initialization
    void Start ()
    {

      



	}
	
	// Update is called once per frame
	void Update ()
    {
        refilljumps();
        HandleHorizontalInput();
        HandleJumpInput();

    }
    private bool OnGround()
    {
       return GroundDetectTrigger.OverlapCollider(GroundContactFilter, GroundHitResults)>0;
    }
    private bool TouchingWall()
    {
        return WallDetectTrigger.OverlapCollider(WallContactFilter, WallHitResults) > 0;
    }
    private void refilljumps()
    {
        if (OnGround())
        {
            RemainingJumps = MaxJumps;
        }
        if (TouchingWall())
        {
            RemainingJumps = MaxJumps;
        }
    }

    private void HandleHorizontalInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && RemainingJumps > 0)
        {
            RemainingJumps--;
            Thisrigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        HorizontalMovement();

        

    }

    private void HorizontalMovement()
    {
        Thisrigidbody.AddForce(Vector2.right * HorizontalInput * AccelrationForce);
        Vector2 ClampedVelocity = Thisrigidbody.velocity;
        ClampedVelocity.x = Mathf.Clamp(Thisrigidbody.velocity.x, -MaxSpeed, MaxSpeed);
        Thisrigidbody.velocity = ClampedVelocity;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
       // RemainingJumps = MaxJumps;
    }


}
