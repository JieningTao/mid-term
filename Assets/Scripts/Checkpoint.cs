using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private Color inactiveColor, activeColor;
    private AudioSource audioSource;
    private bool isactivated = false;
    private SpriteRenderer spriteRenderer;
    private Vector3 shape;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shape = transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }
    private void UpdateColor()
    {
        Color color = inactiveColor;
        if (isactivated)
            color = activeColor;

        spriteRenderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

            if (collision.gameObject.CompareTag("Player") && !isactivated)
            {
                Debug.Log("Check Point Reached!");
                PlayerMoveScript player = collision.GetComponent<PlayerMoveScript>();
                player.setcurrentcheckpoint(this);
            
                audioSource.Play();
            }
        
    }

    public void setisactivated(bool value)
    {
        isactivated = value;
        UpdateColor();
    }
}
