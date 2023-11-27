using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    public float interactionDistance = 2f; // Adjust this distance as needed
    public Text interactText; // Reference to a UI Text component
    public CollectibleCounter collectibleCounter;

    private bool isPlayerFacingObject = false;

    void Start()
    {
        // Ensure the reference to the UI Text component is set
        if (interactText == null)
        {
            Debug.LogError("InteractText reference not set on Collectible script!");
        }
        else
        {
            // Initially hide the interactText
            interactText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Rotate the collectible item
        transform.localRotation = Quaternion.Euler(360f, Time.time * 100f, 0);

        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionDistance);
        bool playerInRange = false;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // Check if the player is facing the object
                isPlayerFacingObject = IsPlayerFacingObject(collider.gameObject.transform);
                playerInRange = true;
                break; // Stop checking once a player is found
            }
        }

        if (playerInRange && isPlayerFacingObject)
        {
            interactText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                CollectItem();
            }
        }
        else
        {
            interactText.gameObject.SetActive(false);
        }
    }

    bool IsPlayerFacingObject(Transform playerTransform)
    {
        // Calculate the angle between the player's forward direction and the direction to the collectible
        Vector3 toCollectible = transform.position - playerTransform.position;
        float angle = Vector3.Angle(playerTransform.forward, toCollectible);

        // Return true if the angle is within an acceptable range
        return angle < 50f; // You can adjust this angle based on your preferences
    }


    void CollectItem()
    {
        // Add your collectible logic here
        // For example, you might want to increase a score or play a sound
        // You can also deactivate or destroy the collectible GameObject

        Debug.Log("Item collected!");

        // Deactivate or destroy the collectible GameObject
        gameObject.SetActive(false);
        // Or you can destroy it: Destroy(gameObject);

        // Hide the interactText after collecting
        interactText.gameObject.SetActive(false);

        collectibleCounter.IncreaseCollectibleCount();

        DoorExit doorExit = FindObjectOfType<DoorExit>();

        if (doorExit != null)
        {
            doorExit.UnlockDoor();
        }
        else
        {
            Debug.LogWarning("DoorExit script not found");
        }
    }
}