using System.Collections.Generic;
using UnityEngine;

public class RecruitService {
    private List<Recruit> playerTeam = new();
    private List<Recruit> recruitPool = new();
    private RecruitGenerator generator = new();

    public void RefreshRecruitPool() {
        recruitPool.Clear();
        for (int i = 0; i < 10; i++) {
            recruitPool.Add(generator.GenerateRandomRecruit());
        }
    }

    public List<Recruit> GetRecruitPool() => recruitPool;
    public List<Recruit> GetPlayerTeam() => playerTeam;

    public void AddToTeam(Recruit recruit) {
        if (!playerTeam.Contains(recruit)) {
            playerTeam.Add(recruit);
            recruitPool.Remove(recruit);
        }
    }

    public void RemoveFromTeam(Recruit recruit) {
        if (playerTeam.Contains(recruit)) {
            playerTeam.Remove(recruit);
        }
    }
    public void ClearTeam() {
        playerTeam.Clear();
    }
}
