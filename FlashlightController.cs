using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    private Light flashlight;
    private bool isPickedUp = false;

    void Start()
    {
        flashlight = GetComponentInChildren<Light>();

        if (flashlight == null)
        {
            Debug.LogError("No Light component found on this GameObject!");
        }
        else
        {
            // Initially disable the flashlight
            flashlight.enabled = false;
        }
    }

    void Update()
    {
        if (isPickedUp)
        {
            // Toggle flashlight on/off when F key is pressed
            if (Input.GetKeyDown(KeyCode.F))
            {
                ToggleFlashlight();
            }
        }
    }

    void ToggleFlashlight()
    {
        if (flashlight != null)
        {
            flashlight.enabled = !flashlight.enabled;
            Debug.Log("Flashlight state toggled. Is now " + (flashlight.enabled ? "on" : "off"));
        }
        else
        {
            Debug.LogError("Light component is null. Make sure the script is attached to a GameObject with a Light component.");
        }
    }

    // Method to set the picked-up state
    public void SetPickedUp(bool pickedUp)
    {
        isPickedUp = pickedUp;
    }
}