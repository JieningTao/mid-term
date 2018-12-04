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
        audioSource = GetComponent<AudioSource>();
        
        shape = transform.localScale;
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
    
    public void Setisactivated(bool value)
    {
        isactivated = value;
        UpdateColor();
    }
}
