using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

// Quits the player when the user hits escape

/// <summary>
/// Input - Quits application when Escape key is pressed
/// For educational use in Animation and Interactivity class.
/// </summary>
public class InputQuitGame : MonoBehaviour
{
	
    void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }

		
    }
}
