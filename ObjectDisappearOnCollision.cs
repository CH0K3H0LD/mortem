using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisappearOnCollision : MonoBehaviour
{
    public GameObject objectToDisappear;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the object to disappear is not null
            if (objectToDisappear != null)
            {
                // Deactivate (hide) the object
                objectToDisappear.SetActive(false);
            }
        }
    }
}