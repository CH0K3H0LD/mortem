using System.Collections;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public GameObject doorClosed;
    public GameObject doorOpened;
    public GameObject interactionText;
    public float interactionDistance = 3.0f;
    private bool isOpened = false;

    void Update()
    {
        if (IsPlayerCloseEnough() && IsPlayerFacingDoor())
        {
            interactionText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleDoor();
            }
        }
        else
        {
            interactionText.SetActive(false);
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

    void ToggleDoor()
    {
        isOpened = !isOpened;
        doorClosed.SetActive(!isOpened);
        doorOpened.SetActive(isOpened);

        // You can add sound effects or other actions here
    }
}
