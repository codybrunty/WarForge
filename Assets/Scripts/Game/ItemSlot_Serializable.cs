using UnityEngine;

public class ItemSlot_Serializable{
    public SlotType slotType;
    public Item_Serializable equippedItem;

    public ItemSlot_Serializable() { }

    public ItemSlot_Serializable(ItemSlot slot) {
        slotType = slot.SlotType;
        if (slot.IsEquipped) {
            equippedItem = new Item_Serializable(slot.EquippedItem);
        }
    }
}
