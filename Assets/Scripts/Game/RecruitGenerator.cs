using System.Collections.Generic;
using UnityEngine;

public class RecruitGenerator {

    private static readonly List<string> namePool = new() { "Rolf", "Mina", "Grub", "Zarn", "Talia" };

    public Recruit GenerateRandomRecruit() {
        var name = GetRandomName();
        var type = GetRandomClassType();
        return new Recruit(name, type);
    }

    private string GetRandomName() => namePool[Random.Range(0, namePool.Count)];

    private RecruitType GetRandomClassType() {
        var values = System.Enum.GetValues(typeof(RecruitType));
        return (RecruitType)values.GetValue(Random.Range(0, values.Length));
    }
    
}
