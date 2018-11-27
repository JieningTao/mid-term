using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupGem : MonoBehaviour
{

    private AudioSource audioSource;
    private SpriteRenderer image;
    private Rigidbody2D thisrigidbody;

    [SerializeField]
    private GameObject Gemmanager;
    private GemManager managerscript;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        image = GetComponent<SpriteRenderer>();
        thisrigidbody = GetComponent<Rigidbody2D>();
        managerscript = Gemmanager.GetComponent<GemManager>();
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Gem Picked Up");
            PlayerMoveScript player = collision.GetComponent<PlayerMoveScript>();
            player.pickupgem(this);
            managerscript.pickupsound();
            this.gameObject.SetActive(false);
        }

    }


    public void pickedup()
    {
        audioSource.Play();
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
