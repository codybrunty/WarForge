using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Recruit{

    [SerializeField] private string name;
    [SerializeField] private RecruitType classType;
    [SerializeField] private RecruitGender gender;
    [SerializeField] private List<ItemSlot> itemSlots = new();

    public string Name => name;
    public RecruitType ClassType => classType;
    public List<ItemSlot> ItemSlots => itemSlots;
    public RecruitGender Gender => gender;

    public Recruit(string name, RecruitType classType, RecruitGender gender) {
        this.name = name;
        this.classType = classType;
        this.gender = gender;

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

public enum RecruitGender {
    Male,
    Female
}
