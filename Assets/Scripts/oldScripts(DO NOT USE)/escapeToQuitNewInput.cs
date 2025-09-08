using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

// Quits the player when the user hits escape

public class escapeToQuitNewInput : MonoBehaviour
{
	
    void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }

		
    }
}
