using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Input - Detects key press and triggers UnityEvent
/// For educational use in Animation and Interactivity class.
/// Connect via UnityEvents in Inspector.
/// </summary>
public class InputKeyPress : MonoBehaviour
{

    public KeyCode  thisKey = KeyCode.Space;

    public UnityEvent onPressEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(thisKey))
        {
            onPressEvent?.Invoke();
        }
    }
}
