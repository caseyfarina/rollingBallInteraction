using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class _keyEventOnDown : MonoBehaviour
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
