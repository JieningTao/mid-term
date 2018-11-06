using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMoveScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D Thisrigidbody;
    [SerializeField]
    private float AccelrationForce =5;
    [SerializeField]
    private float JumpForce = 15;
    [SerializeField]
    private float WallJumpForce = 15;
    [SerializeField]
    private float MaxSpeed = 20;
    [SerializeField]
    private int MaxExtraJumps = 1;
    [SerializeField]
    private ContactFilter2D GroundContactFilter;
    [SerializeField]
    private Collider2D GroundDetectTrigger;
    [SerializeField]
    private ContactFilter2D WallContactFilter;
    [SerializeField]
    private Collider2D LeftWallDetectTrigger;
    [SerializeField]
    private Collider2D RightWallDetectTrigger;


    private PhysicsMaterial2D playermovingPM, playerstoppingPM;
    [SerializeField]
    private Collider2D playergroundcollider;
    [SerializeField]
    private Text deadText;

    private Animator anim;
    private bool IsDead;
    private AudioSource audioSource;
    private float HorizontalInput;
    private int ExtraJumps;
    private Collider2D[] GroundHitResults = new Collider2D[16];
    private Collider2D[] LeftWallHitResults = new Collider2D[16];
    private Collider2D[] RightWallHitResults = new Collider2D[16];
    private Checkpoint currentCheckpoint;
    bool facingRight = true;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();



    }
	
	// Update is called once per frame
	void Update ()
    {
        refilljumps();
        HandleHorizontalInput();
        HandleJumpInput();

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
            respawn();
        }
        HandleAnimator();

    }

    private bool OnGround()
    {
       return GroundDetectTrigger.OverlapCollider(GroundContactFilter, GroundHitResults)>0;
    }
    private char TouchingWall()
    {
        if (RightWallDetectTrigger.OverlapCollider(WallContactFilter, RightWallHitResults) > 0)
            return 'R';
        if (LeftWallDetectTrigger.OverlapCollider(WallContactFilter, LeftWallHitResults) > 0)
            return 'L';
        
        return 'N';
    }
    private void refilljumps()
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
        //if (OnGround() || TouchingWall())
            HorizontalInput = Input.GetAxisRaw("Horizontal");
        //else
        //HorizontalInput = 0;
        if (HorizontalInput > 0 && !facingRight)
            Flip();
        else if (HorizontalInput < 0 && facingRight)
            Flip();
    }

    private void HandleAnimator()
    {   
        anim.SetBool("IsDead", IsDead);
        if (!IsDead)
        {
            anim.SetBool("OnGround", OnGround());
            anim.SetFloat("V.Speed", Thisrigidbody.velocity.y);
            anim.SetFloat("H.Speed", Mathf.Abs(Thisrigidbody.velocity.x));
        }
        
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && TouchingWall()=='L' && !OnGround())
        {
            Thisrigidbody.AddForce(Vector2.up * JumpForce + Vector2.left * WallJumpForce, ForceMode2D.Impulse);
            audioSource.Play();
        }
        else if (Input.GetButtonDown("Jump") && TouchingWall() == 'R' && !OnGround())
        {
            Thisrigidbody.AddForce(Vector2.up * JumpForce + Vector2.right * WallJumpForce, ForceMode2D.Impulse);
            audioSource.Play();
        }
        else if (Input.GetButtonDown("Jump") && OnGround())
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
       // ExtraJumps = MaxExtraJumps;
    }

    private void Updatephysicsmaterial()
    {
        if (Mathf.Abs(HorizontalInput) > 0)
            playergroundcollider.sharedMaterial = playermovingPM;
        else
            playergroundcollider.sharedMaterial = playerstoppingPM;
        
    }
    public void setcurrentcheckpoint(Checkpoint newcurrentcheckpoint)
    {
        if (currentCheckpoint != null)
            currentCheckpoint.setisactivated(false);

        currentCheckpoint = newcurrentcheckpoint;
        currentCheckpoint.setisactivated(true);
    }
    public void respawn()
    {
        deadText.text = " ";
        IsDead = false;



        Thisrigidbody.velocity = Vector2.zero;

        if(currentCheckpoint == null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
            transform.position = currentCheckpoint.transform.position;
    }

    public void killed()
    {
        deadText.text = "You Died! press E to respawn";
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
