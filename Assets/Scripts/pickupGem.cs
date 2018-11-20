using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupGem : MonoBehaviour
{

    [SerializeField]
    private Collider2D playercollider;




    // Use this for initialization
    void Start ()
    {
        playercollider = GetComponent<PolygonCollider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
