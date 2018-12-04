using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGem : MonoBehaviour
{
    [SerializeField]
    private GameObject Gemmanager;
    
    private AudioSource audioSource;
    private SpriteRenderer image;
    private Rigidbody2D thisrigidbody;
    private GemManager managerscript;

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
}
