using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Input - 3D trigger zone that detects collisions by tag
/// For educational use in Animation and Interactivity class.
/// Connect via UnityEvents in Inspector.
/// </summary>
public class InputTriggerZone : MonoBehaviour
{
    public string triggerObjectTag = "Player";
    public UnityEvent onTriggerEnterEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == triggerObjectTag)
        {

            onTriggerEnterEvent?.Invoke();
        }
        
    }
}
