using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Recruit{

    [SerializeField] private string name;
    [SerializeField] private RecruitType classType;
    [SerializeField] private List<ItemSlot> itemSlots = new();

    public string Name => name;
    public RecruitType ClassType => classType;
    public List<ItemSlot> ItemSlots => itemSlots;

    public Recruit(string name, RecruitType classType) {
        this.name = name;
        this.classType = classType;

        var slotLayout = GameConfigs.ClassConfig.GetSlotsForClass(classType);
        foreach (var slot in slotLayout) {
            itemSlots.Add(new ItemSlot(slot));
        }
    }

}

public enum RecruitType {
    Peasant,
    Fighter,
    Soldier,
    General,
    Warlord
}
