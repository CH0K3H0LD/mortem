using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public DoorKey door;
    public GameObject interactionText;
    public float interactionDistance = 3.0f;
    private bool isPlayerFacingObject = false;

    private void Update()
    {
        if (isPlayerFacingObject && Input.GetKeyDown(KeyCode.E))
        {
            // Assuming you want the key to be automatically consumed when picked up
            // If not, you can modify this part accordingly
            Destroy(gameObject);

            // Unlock the door
            door.UnlockDoor();

            // You can add sound effects or other actions here

            // Hide the interaction text after interaction
            HideInteractionText();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowInteractionText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideInteractionText();
        }
    }

    private void ShowInteractionText()
    {
        interactionText.SetActive(true);
        isPlayerFacingObject = true;
    }

    private void HideInteractionText()
    {
        interactionText.SetActive(false);
        isPlayerFacingObject = false;
    }

    private void OnDrawGizmos()
    {
        // Visualize the interaction distance in the Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}