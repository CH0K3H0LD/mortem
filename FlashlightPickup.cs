using UnityEngine;
using UnityEngine.UI;

public class FlashlightPickup : MonoBehaviour
{
    public float interactionDistance = 2f; // Adjust this distance as needed
    public Text interactText; // Reference to a UI Text component
    private bool isPickedUp = false;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, interactionDistance) && hit.collider.gameObject == gameObject)
        {
            interactText.text = "[E]";
            interactText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && !isPickedUp)
            {
                PickUpFlashlight();
            }
        }
        else
        {
            interactText.gameObject.SetActive(false);
        }
    }

    void PickUpFlashlight()
    {
        // Disable the flashlight's renderer and collider
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Attach the flashlight to the player (optional)
        // You can modify this part based on your game design

        isPickedUp = true;
        Debug.Log("Flashlight picked up!");

        // Notify the player's FlashlightController to enable the flashlight controls
        FlashlightController flashlightController = FindObjectOfType<FlashlightController>();
        if (flashlightController != null)
        {
            flashlightController.SetPickedUp(true);
        }
        else
        {
            Debug.LogError("FlashlightController not found in the scene.");
        }
    }
}