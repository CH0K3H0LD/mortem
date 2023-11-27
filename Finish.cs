using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    // The name of the scene to load
    public string End;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadNextSceneFunction();
        }
    }

    private void LoadNextSceneFunction()
    {
        // Load the next scene based on the name provided
        SceneManager.LoadScene(End);
    }
}