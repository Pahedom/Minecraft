using System;
using UnityEngine;

public class QuickSlots : Debuggable
{
    public static readonly int numberOfSlots = 9;

    public Block[] items => _items;

    [SerializeField]
    private Block[] _items = new Block[numberOfSlots];

    private BlockInteractions _blockInteractions;

    private int _selectedSlot;

    public Action<int> OnSlotSelected;
    public Action<int> OnItemChanged;

    private void Start()
    {
        _blockInteractions = GetComponentInParent<BlockInteractions>();

        SelectSlot(0);
    }

    private void OnValidate()
    {
        if (_items.Length != numberOfSlots)
        {
            Debug.LogWarning("You cannot change the length of this array!");
            Array.Resize(ref _items, numberOfSlots);
        }
    }

    public void SelectSlot(int slot)
    {
        if (!slot.IsBetween(0, numberOfSlots - 1))
        {
            DebugError("Couldn't select slot: out of range");
            return;
        }

        _selectedSlot = slot;
        _blockInteractions.blockToCreate = _items[slot];
        OnSlotSelected?.Invoke(slot);
    }

    public void SelectNext()
    {
        int newSlot = _selectedSlot + 1;

        if (newSlot >= numberOfSlots)
        {
            newSlot = 0;
        }

        SelectSlot(newSlot);
    }

    public void SelectPrevious()
    {
        int newSlot = _selectedSlot - 1;

        if (newSlot < 0)
        {
            newSlot = numberOfSlots - 1;
        }

        SelectSlot(newSlot);
    }

    public void ChangeItem(int slot, Block newItem)
    {
        if (!slot.IsBetween(0, numberOfSlots - 1))
        {
            DebugError("Couldn't change item: invalid slot");
            return;
        }

        _items[slot] = newItem;
        OnItemChanged?.Invoke(slot);
    }
}