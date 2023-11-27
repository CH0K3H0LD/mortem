using UnityEngine;
using UnityEngine.UI;

public class CollectibleCounter : MonoBehaviour
{
    public Text collectibleCountText; // Reference to the UI Text component
    private int collectibleCount = 0; // Counter for collected items

    void Start()
    {
        // Ensure the reference to the UI Text component is set
        if (collectibleCountText == null)
        {
            Debug.LogError("CollectibleCountText reference not set on CollectibleCounter script!");
        }
        else
        {
            UpdateCollectibleCountText(); // Update the text initially
        }
    }

    public void IncreaseCollectibleCount()
    {
        collectibleCount++;
        UpdateCollectibleCountText();
    }

    void UpdateCollectibleCountText()
    {
        if (collectibleCountText != null)
        {
            collectibleCountText.text = "Brains: " + collectibleCount;
        }
    }
}