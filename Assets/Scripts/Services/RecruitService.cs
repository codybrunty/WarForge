using System.Collections.Generic;
using UnityEngine;

public class RecruitService {
    private List<Recruit> team = new();
    private List<Recruit> recruits = new();
    private List<Recruit> graveyard = new();

    public List<Recruit> GetRecruits() => recruits;
    public List<Recruit> GetTeam() => team;
    public List<Recruit> GetGraveyard() => graveyard;

    public void RefreshRecruitPool(int poolSize) {
        recruits.Clear();
        for (int i = 0; i < poolSize; i++) {
            recruits.Add(GenerateRandomRecruit());
        }
    }

    public void AddToTeam(Recruit recruit) {
        if (!team.Contains(recruit)) {
            team.Add(recruit);
            recruits.Remove(recruit);
        }
    }

    public void AddToGraveyard(Recruit recruit) {
        if (!graveyard.Contains(recruit)) {
            graveyard.Add(recruit);
            team.Remove(recruit);
        }
    }

    public Recruit GenerateRandomRecruit() {
        RecruitType type = GetRandomClassType();
        RecruitGender gender = GetRandomGender(); 
        string name = gender == RecruitGender.Male? GameBootstrapper.NameGeneration.GetMaleName(): GameBootstrapper.NameGeneration.GetFemaleName();
        return new Recruit(name, type, gender);
    }

    private RecruitType GetRandomClassType() {
        var values = System.Enum.GetValues(typeof(RecruitType));
        return (RecruitType)values.GetValue(Random.Range(0, values.Length));
    }
    private RecruitGender GetRandomGender() {
        var values = System.Enum.GetValues(typeof(RecruitGender));
        return (RecruitGender)values.GetValue(Random.Range(0, values.Length));
    }
}
