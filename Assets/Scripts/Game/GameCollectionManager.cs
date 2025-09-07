using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// Game - Manages collection/scoring system with UI display and threshold events
/// For educational use in Animation and Interactivity class.
/// Connect via UnityEvents in Inspector.
/// </summary>
public class GameCollectionManager : MonoBehaviour
{ 
    [SerializeField] private int currentValue = 0;
    [SerializeField] private int threshold = 10;
    [SerializeField] private TextMeshProUGUI displayText;

    public UnityEvent onThresholdReached;
    public UnityEvent onValueChanged;

    private void Start()
    {
        UpdateDisplay();
    }

    public void Increment(int amount = 1)
    {
        currentValue += amount;
        UpdateDisplay();
        CheckThreshold();
    }

    public void Decrement(int amount = 1)
    {
        currentValue -= amount;
        UpdateDisplay();
        CheckThreshold();
    }

    private void UpdateDisplay()
    {
        if (displayText != null)
        {
            displayText.text = currentValue.ToString();
        }
        onValueChanged.Invoke();
    }

    private void CheckThreshold()
    {
        if (currentValue >= threshold)
        {
            onThresholdReached.Invoke();
        }
    }
}

