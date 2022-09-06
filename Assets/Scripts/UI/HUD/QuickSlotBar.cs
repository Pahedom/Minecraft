using System;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotBar : Debuggable
{
    public Image[] slots = new Image[QuickSlots.numberOfSlots];
    public Transform slotIndicator;

    private QuickSlots _player;

    private void Awake()
    {
        _player = FindObjectOfType<QuickSlots>();

        _player.OnSlotSelected += SelectSlot;
        _player.OnItemChanged += UpdateItemDisplay;

        UpdateItemsDisplay();
    }

    private void OnValidate()
    {
        if (slots.Length != QuickSlots.numberOfSlots)
        {
            Debug.LogWarning("You cannot change the length of this array!");
            Array.Resize(ref slots, QuickSlots.numberOfSlots);
        }
    }

    public void SelectSlot(int slot)
    {
        slotIndicator.position = slots[slot].transform.position;
    }

    private void UpdateItemDisplay(int slot)
    {
        if (_player.items[slot] != null)
        {
            slots[slot].enabled = true;
            slots[slot].sprite = _player.items[slot].icon;
            slots[slot].SetNativeSize();
        }
        else
        {
            slots[slot].enabled = false;
            slots[slot].sprite = null;
        }
    }

    private void UpdateItemsDisplay()
    {
        for (int i = 0; i < QuickSlots.numberOfSlots; i++)
        {
            UpdateItemDisplay(i);
        }
    }
}