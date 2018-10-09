using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private float inactiverotationspeed = 100,activerotationspeed = 300;

    [SerializeField]
    private float inactiveScale = 0.75f, activeScale = 1.3f;

    [SerializeField]
    private Color inactiveColor, activeColor;

    private bool isactivated = false;
    private SpriteRenderer spriteRenderer;
    private Vector3 shape;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shape = transform.localScale;
    }
    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateScale()
    {
        float scale = inactiveScale;
        if (isactivated)
           scale = activeScale;

        transform.localScale = shape * scale;
        
    }
    private void UpdateColor()
    {
        Color color = inactiveColor;
        if (isactivated)
            color = activeColor;

        spriteRenderer.color = color;
    }

    private void UpdateRotation()
    {
        float rotationspeed = inactiverotationspeed;
        if (isactivated)
            rotationspeed = activerotationspeed;
            


        transform.Rotate(Vector3.up*rotationspeed*Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Check Point Reached!");
                PlayerMoveScript player = collision.GetComponent<PlayerMoveScript>();
                player.setcurrentcheckpoint(this);
            }
        
    }

    public void setisactivated(bool value)
    {
        isactivated = value;
        UpdateScale();
        UpdateColor();
    }
}
