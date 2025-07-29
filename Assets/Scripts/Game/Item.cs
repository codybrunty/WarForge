using System;
using UnityEngine;

[Serializable]
public class Item {
    [SerializeField] private string name;
    [SerializeField] private SlotType slotType;

    public string Name => name;
    public SlotType SlotType => slotType;

    public Item(string name, SlotType slotType) {
        this.name = name;
        this.slotType = slotType;
    }
}
