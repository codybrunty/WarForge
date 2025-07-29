using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClassConfig", menuName = "Scriptable Objects/ClassConfig")]
public class ClassConfig : ScriptableObject {
    [System.Serializable]
    public class ClassSlotDefinition {
        public RecruitType classType;
        public List<SlotType> slotLayout;
    }

    [SerializeField]
    private List<ClassSlotDefinition> classSlotDefinitions = new();

    private Dictionary<RecruitType, List<SlotType>> _slotMap;

    public List<SlotType> GetSlotsForClass(RecruitType type) {
        if (_slotMap == null) {
            BuildSlotMap();
        }

        return _slotMap.TryGetValue(type, out var slots)
            ? new List<SlotType>(slots) // Return a copy so callers can't modify the original
            : new List<SlotType>();
    }

    private void BuildSlotMap() {
        _slotMap = new Dictionary<RecruitType, List<SlotType>>();
        foreach (var def in classSlotDefinitions) {
            _slotMap[def.classType] = def.slotLayout;
        }
    }
}
