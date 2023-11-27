using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class DoorExit : MonoBehaviour
{
    public GameObject doorClosed;
    public GameObject doorOpened;
    public GameObject unlockedInteractionText;
    public GameObject lockedInteractionText;
    public float interactionDistance = 3.0f;
    private bool isOpened = false;
    private bool isLocked = true; // Initialize the door as locked

    void Update()
    {
        if (IsPlayerCloseEnough() && IsPlayerFacingDoor())
        {
            if (isLocked)
            {
                lockedInteractionText.SetActive(true);
                unlockedInteractionText.SetActive(false);
            }
            else
            {
                unlockedInteractionText.SetActive(true);
                lockedInteractionText.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                TryToggleDoor();
            }
        }
        else
        {
            lockedInteractionText.SetActive(false);
            unlockedInteractionText.SetActive(false);
        }
    }

    bool IsPlayerCloseEnough()
    {
        Transform player = Camera.main.transform;
        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= interactionDistance;
    }

    bool IsPlayerFacingDoor()
    {
        Transform player = Camera.main.transform;
        Vector3 toDoor = transform.position - player.position;
        float angle = Vector3.Angle(player.forward, toDoor.normalized);
        return angle < 30f; // Adjust the angle threshold as needed
    }

    public void TryToggleDoor()
    {
        if (!isLocked)
        {
            ToggleDoor();
        }
        else
        {
            // Door is locked, you can add a message or sound effect indicating that it's locked
            Debug.Log("The door is locked.");
        }
    }

    void ToggleDoor()
    {
        isOpened = !isOpened;
        doorClosed.SetActive(!isOpened);
        doorOpened.SetActive(isOpened);

        // You can add sound effects or other actions here
    }

    // Add a method to unlock the door if needed
    public void UnlockDoor()
    {
        isLocked = false;
    }
}