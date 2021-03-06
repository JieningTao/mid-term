﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMoveScript : MonoBehaviour
{
#region Plugin Variables
    [SerializeField]
    private Rigidbody2D Thisrigidbody;
    [SerializeField]
    [Tooltip("The speed that players start to move")]
    private float AccelrationForce =5;
    [SerializeField]
    private float JumpForce = 15;
    [SerializeField]
    private float WallJumpForce = 15;
    [SerializeField]
    private float MaxSpeed = 20;
    [SerializeField]
    [Tooltip("Amount of in-air jumps avaliable to player")]
    private int MaxExtraJumps = 1;
    [SerializeField]
    private ContactFilter2D GroundContactFilter;
    [SerializeField]
    private Collider2D GroundDetectTrigger;
    [SerializeField]
    private ContactFilter2D WallContactFilter;
    [SerializeField]
    private Collider2D FrontWallDetectTrigger;
    [SerializeField]
    private Collider2D BackWallDetectTrigger;
    [SerializeField]
    private PhysicsMaterial2D playermovingPM, playerstoppingPM, playerwallclingPM;
    [SerializeField]
    private Collider2D playergroundcollider;
    [SerializeField]
    private Text deadText;
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    [Tooltip("How much the player slows down in the air")]
    private float airspeedreduction;
    [SerializeField]
    private ParticleSystem wallsmoke;
#endregion

    private int Score;
    private Animator anim;
    private bool IsDead;
    private AudioSource audioSource;
    private float HorizontalInput;
    private int ExtraJumps;
    private Collider2D[] GroundHitResults = new Collider2D[16];
    private Collider2D[] LeftWallHitResults = new Collider2D[16];
    private Collider2D[] RightWallHitResults = new Collider2D[16];
    private Checkpoint currentCheckpoint;
    private PickupGem Gem;
    bool facingRight = true;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        Score = 0;
    }
	
    void Update ()
    {
        if (!IsDead)
        {
            RefillJumps();
            HandleHorizontalInput();
            HandleJumpInput();
        }
    }
    
    private void FixedUpdate()
    {
        if (!IsDead)
        {
            HorizontalMovement();
            Updatephysicsmaterial();
        }
        else if(Input.GetButtonDown("Activate"))
        {
            Respawn();
        }
        HandleAnimator();
    }

    private bool OnGround()
    {
       return GroundDetectTrigger.OverlapCollider(GroundContactFilter, GroundHitResults)>0;
    }
    
    private char TouchingWall()
    {
        if (FrontWallDetectTrigger.OverlapCollider(WallContactFilter, LeftWallHitResults) > 0 )
        {
            if(!facingRight)
            return 'L';
            else
            return 'R';
        }
        if (BackWallDetectTrigger.OverlapCollider(WallContactFilter, RightWallHitResults) > 0 )
        {
            if (!facingRight)
                return 'R';
            else
                return 'L';
        }
        return 'N';
    }

    private void RefillJumps()
    {
        if (OnGround())
        {
            ExtraJumps = MaxExtraJumps;
        }
        if (TouchingWall()!='N')
        {
            ExtraJumps = MaxExtraJumps;
        }
    }

    private void HandleHorizontalInput()
    {
            HorizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void HandleAnimator()
    {   
        anim.SetBool("IsDead", IsDead);
        if (!IsDead)
        {
            anim.SetBool("OnGround", OnGround());
            anim.SetFloat("V.Speed", Thisrigidbody.velocity.y);
            anim.SetFloat("H.Speed", Mathf.Abs(Thisrigidbody.velocity.x));
            if(Thisrigidbody.velocity.x>0 && !facingRight)
                Flip();
            else if (Thisrigidbody.velocity.x < 0 && facingRight)
                Flip();
            if (TouchingWall() != 'N'&& !OnGround() && Thisrigidbody.velocity.y>0.1)
            {
                anim.SetBool("WallCling", true);
                wallsmoke.enableEmission = true;
            }
            else if(TouchingWall() == 'N' || OnGround())
            {
                anim.SetBool("WallCling", false);
                wallsmoke.enableEmission = false;
            }
        }
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && OnGround())
        {
            Thisrigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            audioSource.Play();
        }
        else if (Input.GetButtonDown("Jump") && !OnGround() && TouchingWall()=='N' && ExtraJumps > 0)
        {
            ExtraJumps--;
            Thisrigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            audioSource.Play();
        }
        else if (Input.GetButtonDown("Jump") && TouchingWall() != 'N' && !OnGround())
        {
            if (TouchingWall() == 'L')
                Thisrigidbody.AddForce(Vector2.up * JumpForce + Vector2.right * WallJumpForce, ForceMode2D.Impulse);
            else
                Thisrigidbody.AddForce(Vector2.up * JumpForce + Vector2.left * WallJumpForce, ForceMode2D.Impulse);
            audioSource.Play();
        }
    }

    private void HorizontalMovement()
    {
        float accelerationToUse = OnGround() ? AccelrationForce : AccelrationForce * airspeedreduction;
        Thisrigidbody.AddForce(Vector2.right * HorizontalInput * accelerationToUse);
        Vector2 ClampedVelocity = Thisrigidbody.velocity;
        ClampedVelocity.x = Mathf.Clamp(Thisrigidbody.velocity.x, -MaxSpeed, MaxSpeed);
        Thisrigidbody.velocity = ClampedVelocity;
    }

    private void Updatephysicsmaterial()
    {
        if (!OnGround() && TouchingWall() != 'N')
            playergroundcollider.sharedMaterial = playerwallclingPM;
        else if (Mathf.Abs(HorizontalInput) > 0)
            playergroundcollider.sharedMaterial = playermovingPM;
        else
            playergroundcollider.sharedMaterial = playerstoppingPM;
    }
    
    public void SetCurrentCheckpoint(Checkpoint newcurrentcheckpoint)
    {
        if (currentCheckpoint != null)
            currentCheckpoint.Setisactivated(false);

        currentCheckpoint = newcurrentcheckpoint;
        currentCheckpoint.Setisactivated(true);
    }

    public void pickupgem(PickupGem NewGem)
    {
        Score++;
        ScoreText.text = "Score: " + Score;      
    }

    public void Respawn()
    {
        deadText.text = " ";
        IsDead = false;

        Thisrigidbody.velocity = Vector2.zero;

        if (currentCheckpoint == null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
            transform.position = currentCheckpoint.transform.position;

        transform.eulerAngles = Vector3.zero;
        Thisrigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    
    public void Killed()
    {
        deadText.text = "You Died!\n press E to respawn";
        IsDead = true;

        Thisrigidbody.constraints = RigidbodyConstraints2D.None;

    }
    
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
