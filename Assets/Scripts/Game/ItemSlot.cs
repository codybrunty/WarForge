using UnityEngine;
using System;

[Serializable]
public class ItemSlot {
    [SerializeField] private SlotType slotType;
    [SerializeField] private Item equippedItem;

    public SlotType SlotType => slotType;
    public Item EquippedItem => equippedItem;
    public bool IsEquipped => equippedItem != null;

    public ItemSlot(SlotType slotType) {
        this.slotType = slotType;
    }

    public void Equip(Item item) {
        if (item.SlotType == slotType) {
            equippedItem = item;
        }
        else {
            Debug.LogWarning($"Cannot equip {item.Name} to {slotType} slot.");
        }
    }

    public void Unequip() {
        equippedItem = null;
    }
}

public enum SlotType {
    Armor,
    Weapon,
    Trinket
}
