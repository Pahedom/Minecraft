using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    SphereCollider myCollider;

    public Movement playerMovement;

    public bool colliding;

    private void Start()
    {
        myCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            colliding = true;

            Debug.Log("colliding");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(myCollider, other);
        }

        if (other.gameObject.CompareTag("Block") && !colliding)
        {
            colliding = true;
            
            playerMovement.GroundCheck(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Block") && playerMovement.GetVelocity().y != 0)
        {
            colliding = false;

            playerMovement.GroundCheck(false);
        }
    }
}
