using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    CapsuleCollider myCollider;

    public Movement playerMovement;

    private void Start()
    {
        myCollider = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(myCollider, other);
        }

        if (other.gameObject.CompareTag("Block"))
        {
            playerMovement.CollisionCheck(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            playerMovement.CollisionCheck(false);
        }
    }
}
