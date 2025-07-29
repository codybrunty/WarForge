using UnityEngine;

public class RecruitPopup_Viewmodel : MonoBehaviour {
    private const string RecruitCardUIRecruitPoolKey = "RecruitCardUI_Recruit";
    private const string RecruitCardUITeamPoolKey = "RecruitCardUI_Team";
    [SerializeField] private Transform recruitsContainer;
    [SerializeField] private Transform teamContainer;

    private void Start() {
        InstantiateRecruitPool();
        RefreshAll();
    }
    private void InstantiateRecruitPool() {
        GameBootstrapper.Recruit.RefreshRecruitPool(10);
    }
    private void RefreshAll() {
        RefreshRecruitsUI();
        RefreshTeamUI();
    }
    private void RefreshRecruitsUI() {
        ClearRecruitsUI();
        var recruits = GameBootstrapper.Recruit.GetRecruits();
        foreach (var recruit in recruits) {
            var card = GameBootstrapper.ObjectPool.GetFromPool(RecruitCardUIRecruitPoolKey);
            card.GetComponent<RecruitCardUI_Recruit>().Setup(recruit);
            card.transform.SetParent(recruitsContainer, false);
        }
    }
    private void ClearRecruitsUI() {
        for (int i = recruitsContainer.childCount - 1; i >= 0; i--) {
            Transform child = recruitsContainer.GetChild(i);
            GameBootstrapper.ObjectPool.ReturnToPool(RecruitCardUIRecruitPoolKey, child.gameObject);
        }
    }
    private void RefreshTeamUI() {
        ClearTeamUI();

        var team = GameBootstrapper.Recruit.GetTeam();
        foreach (var recruit in team) {
            var card = GameBootstrapper.ObjectPool.GetFromPool(RecruitCardUITeamPoolKey);
            card.GetComponent<RecruitCardUI>().Setup(recruit);
            card.transform.SetParent(teamContainer, false);
        }
    }

    private void ClearTeamUI() {
        for (int i = teamContainer.childCount - 1; i >= 0; i--) {
            Transform child = teamContainer.GetChild(i);
            GameBootstrapper.ObjectPool.ReturnToPool(RecruitCardUITeamPoolKey, child.gameObject);
        }
    }
    private void OnEnable() {
        EventBus.Subscribe<RecruitSelectedEvent>(OnRecruitClicked);
    }

    private void OnDisable() {
        EventBus.Unsubscribe<RecruitSelectedEvent>(OnRecruitClicked);
    }

    private void OnRecruitClicked(RecruitSelectedEvent evt) {
        GameBootstrapper.Recruit.AddToTeam(evt.Recruit);
        RefreshAll();
    }
}
