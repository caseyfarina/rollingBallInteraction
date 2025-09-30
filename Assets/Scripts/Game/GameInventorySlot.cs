using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// Game - Inventory slot extending collection system with item-specific functionality
/// For educational use in Animation and Interactivity class.
/// Connect via UnityEvents in Inspector.
/// </summary>
public class GameInventorySlot : GameCollectionManager
{
    [Header("Inventory Slot")]
    [SerializeField] private string itemType = "Item";
    [SerializeField] private int maxCapacity = 10;

    [Header("Inventory Events")]
    public UnityEvent onItemUsed;
    public UnityEvent onSlotEmpty;
    public UnityEvent onSlotFull;
    public UnityEvent onCapacityReached;

    public string ItemType => itemType;
    public int MaxCapacity => maxCapacity;
    public bool IsEmpty => currentValue <= 0;
    public bool IsFull => currentValue >= maxCapacity;

    public new void Increment(int amount = 1)
    {
        int previousValue = currentValue;
        int newValue = Mathf.Clamp(currentValue + amount, 0, maxCapacity);

        if (newValue != currentValue)
        {
            currentValue = newValue;
            UpdateDisplay();
            CheckThreshold();

            if (currentValue >= maxCapacity && previousValue < maxCapacity)
            {
                onSlotFull.Invoke();
                onCapacityReached.Invoke();
            }
        }
    }

    public new void Decrement(int amount = 1)
    {
        int previousValue = currentValue;
        currentValue = Mathf.Max(0, currentValue - amount);
        UpdateDisplay();
        CheckThreshold();

        if (currentValue <= 0 && previousValue > 0)
        {
            onSlotEmpty.Invoke();
        }
    }

    public void UseItem(int amount = 1)
    {
        if (currentValue >= amount)
        {
            Decrement(amount);
            onItemUsed.Invoke();
        }
    }

    public bool CanAddItem(int amount = 1)
    {
        return currentValue + amount <= maxCapacity;
    }

    public void SetItemType(string newItemType)
    {
        itemType = newItemType;
    }

    public void SetMaxCapacity(int newCapacity)
    {
        maxCapacity = Mathf.Max(1, newCapacity);
        if (currentValue > maxCapacity)
        {
            currentValue = maxCapacity;
            UpdateDisplay();
        }
    }
}