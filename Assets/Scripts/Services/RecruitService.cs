using System.Collections.Generic;
using UnityEngine;

public class RecruitService {
    private List<Recruit> team = new();
    private List<Recruit> recruits = new();
    private List<Recruit> graveyard = new();

    public List<Recruit> GetRecruits() => recruits;
    public List<Recruit> GetTeam() => team;
    public List<Recruit> GetGraveyard() => graveyard;
    private const string RecruitServiceTeam_SaveLoadKey = "Team";
    private const string RecruitServiceRecruits_SaveLoadKey = "Recruits";

    public RecruitService() {
        EventBus.Subscribe<NewDayEvent>(OnNewDay);
        EventBus.Subscribe<FirstDayEvent>(OnFirstDay);
    }

    public void UpdateRecruitPool() {
        int recruitsToAdd = Mathf.Min(GameConfigs.RecruitPoolConfig.IncrementRecruitPoolBy, GameConfigs.RecruitPoolConfig.RecruitPoolMaxSize - recruits.Count);
        for (int i = 0; i < recruitsToAdd; i++) {
            recruits.Add(GenerateRandomRecruit());
        }
        SaveRecruits();
    }

    public void AddToTeam(Recruit recruit) {
        if (!team.Contains(recruit) && team.Count < GameConfigs.TeamConfig.MaxTeamSize) {
            team.Add(recruit);
            recruits.Remove(recruit);
            SaveTeam();
            SaveRecruits();
        }
    }

    public void AddToGraveyard(Recruit recruit) {
        if (!graveyard.Contains(recruit)) {
            graveyard.Add(recruit);
            team.Remove(recruit);
            SaveTeam();
        }
    }

    #region Data
    public void SaveTeam() {
        var teamData = new List<Recruit_Serializable>();
        foreach (var recruit in team) {
            teamData.Add(new Recruit_Serializable(recruit));
        }
        GameBootstrapper.SaveLoad.Save(RecruitServiceTeam_SaveLoadKey, teamData);
    }
    public void LoadTeam() {
        var teamData = GameBootstrapper.SaveLoad.Load<List<Recruit_Serializable>>(RecruitServiceTeam_SaveLoadKey);
        team.Clear();

        if (teamData != null) {
            foreach (var recruit in teamData) {
                team.Add(recruit.ToRecruit());
            }
        }
    }
    public void DeleteTeam() {
        GameBootstrapper.SaveLoad.Delete(RecruitServiceTeam_SaveLoadKey);
    }
    public void SaveRecruits() {
        var recruitsData = new List<Recruit_Serializable>();
        foreach (var recruit in recruits) {
            recruitsData.Add(new Recruit_Serializable(recruit));
        }
        GameBootstrapper.SaveLoad.Save(RecruitServiceRecruits_SaveLoadKey, recruitsData);
    }

    public void LoadRecruits() {
        var recruitsData = GameBootstrapper.SaveLoad.Load<List<Recruit_Serializable>>(RecruitServiceRecruits_SaveLoadKey);
        recruits.Clear();

        if (recruitsData != null) {
            foreach (var recruit in recruitsData) {
                recruits.Add(recruit.ToRecruit());
            }
        }
    }
    public void DeleteRecruits() {
        GameBootstrapper.SaveLoad.Delete(RecruitServiceRecruits_SaveLoadKey);
    }
    #endregion

    #region Recruit
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
    #endregion

    #region Events
    private void OnNewDay(NewDayEvent evt) {
        UpdateRecruitPool();
    }
    private void OnFirstDay(FirstDayEvent evt) {
        UpdateRecruitPool();
    }
    #endregion

}
