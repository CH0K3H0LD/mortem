using UnityEngine;
using UnityEngine.UI;

public class InteractiveSpotlight : MonoBehaviour
{
    private Light spotlight;
    private bool isSpotlightOn = false;

    [SerializeField]
    private float interactionDistance = 3f;

    [SerializeField]
    private Text interactionText;

    void Start()
    {
        // Find the Light component on this object or its children
        spotlight = GetComponentInChildren<Light>();

        if (spotlight == null)
        {
            Debug.LogError("Light component not found on the object or its children.");
        }
        else
        {
            // Ensure the spotlight starts turned off
            spotlight.enabled = false;
        }

        // Disable the interaction text initially
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (IsPlayerCloseEnough() && IsPlayerFacingObject())
        {
            // Show the interaction text only when the player is looking at the object
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true);
            }

            // Check for player input to toggle the spotlight
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E key pressed");
                ToggleSpotlight();
            }
        }
        else
        {
            // Hide the interaction text when the player is not close to the object or not facing it
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }

    void ToggleSpotlight()
    {
        // Toggle the spotlight on and off
        if (spotlight != null)
        {
            isSpotlightOn = !isSpotlightOn;
            spotlight.enabled = isSpotlightOn;
            Debug.Log("Spotlight toggled: " + (isSpotlightOn ? "ON" : "OFF"));
        }
    }

    bool IsPlayerCloseEnough()
    {
        // Check if the player is close enough to interact with the object
        Transform player = Camera.main.transform; // Assuming the camera is the player's point of view
        float distance = Vector3.Distance(transform.position, player.position);

        return distance <= interactionDistance;
    }

    bool IsPlayerFacingObject()
    {
        // Check if the player is facing the object
        Transform player = Camera.main.transform; // Assuming the camera is the player's point of view
        Vector3 toObject = transform.position - player.position;
        float angle = Vector3.Angle(player.forward, toObject.normalized);

        // Adjust the angle threshold as needed
        return angle < 20f; // Adjust this angle threshold based on your preferences
    }
}