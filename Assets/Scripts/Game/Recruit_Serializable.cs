using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Recruit_Serializable{
    public string name;
    public RecruitType classType;
    public RecruitGender gender;
    public List<ItemSlot_Serializable> itemSlots = new();

    public Recruit_Serializable() { }

    public Recruit_Serializable(Recruit recruit) {
        name = recruit.Name;
        classType = recruit.ClassType;
        gender = recruit.Gender;

        foreach (var slot in recruit.ItemSlots) {
            itemSlots.Add(new ItemSlot_Serializable(slot));
        }
    }

    public Recruit ToRecruit() {
        var recruit = new Recruit(name, classType, gender);
        for (int i = 0; i < recruit.ItemSlots.Count && i < itemSlots.Count; i++) {
            var serialSlot = itemSlots[i];
            var targetSlot = recruit.ItemSlots[i];

            if (serialSlot.equippedItem != null) {
                targetSlot.Equip(serialSlot.equippedItem.ToItem());
            }
        }
        return recruit;
    }
}
