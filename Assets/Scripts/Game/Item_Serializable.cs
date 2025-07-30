using UnityEngine;

public class Item_Serializable{
    public string name;
    public SlotType slotType;

    public Item_Serializable() { }

    public Item_Serializable(Item item) {
        name = item.Name;
        slotType = item.SlotType;
    }

    public Item ToItem() {
        return new Item(name, slotType);
    }
}
