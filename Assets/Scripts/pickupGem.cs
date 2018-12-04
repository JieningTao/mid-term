using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGem : MonoBehaviour
{
    [SerializeField]
    private Collider2D playercollider;

    void Start ()
    {
        playercollider = GetComponent<PolygonCollider2D>();
    }
}
