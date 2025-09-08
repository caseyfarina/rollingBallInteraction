using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Action - Restarts the current scene when R key is pressed
/// For educational use in Animation and Interactivity class.
/// </summary>
public class ActionRestartScene : MonoBehaviour
{

    public KeyCode restartKey = KeyCode.R;
    void Update()
    {
        if (Input.GetKey(restartKey))
        {
            Restart();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}