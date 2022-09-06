using System;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotBar : Debuggable
{
    private const int _numberOfSlots = 9;

    public Image[] slots = new Image[_numberOfSlots];
    public Transform slotIndicator;

    public Block[] items = new Block[_numberOfSlots];

    private BlockInteractions _player;

    private int _selectedSlot;

    private void Start()
    {
        _player = FindObjectOfType<BlockInteractions>();

        SetSelectSlot(0);
        UpdateItemsDisplay();
    }

    private void OnValidate()
    {
        if (slots.Length != _numberOfSlots)
        {
            Debug.LogWarning("You cannot change the length of this array!");
            Array.Resize(ref slots, _numberOfSlots);
        }
        if (items.Length != _numberOfSlots)
        {
            Debug.LogWarning("You cannot change the length of this array!");
            Array.Resize(ref items, _numberOfSlots);
        }
    }

    public void SetSelectSlot(int slot)
    {
        if (!slot.IsBetween(0, _numberOfSlots - 1))
        {
            DebugError("Couldn't select slot: out of range");
            return;
        }

        slotIndicator.position = slots[slot].transform.position;
        _selectedSlot = slot;
        _player.blockToCreate = items[slot];
    }

    public void SelectNext()
    {
        int newSlot = _selectedSlot + 1;

        if (newSlot >= _numberOfSlots)
        {
            newSlot = 0;
        }

        SetSelectSlot(newSlot);
    }

    public void SelectPrevious()
    {
        int newSlot = _selectedSlot - 1;

        if (newSlot < 0)
        {
            newSlot = _numberOfSlots - 1;
        }

        SetSelectSlot(newSlot);
    }

    private void UpdateItemDisplay(int slot)
    {
        if (items[slot] != null)
        {
            slots[slot].enabled = true;
            slots[slot].sprite = items[slot].icon;
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
        for (int i = 0; i < _numberOfSlots; i++)
        {
            UpdateItemDisplay(i);
        }
    }
}