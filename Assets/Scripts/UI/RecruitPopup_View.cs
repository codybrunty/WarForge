using UnityEngine;

public class RecruitPopup_View : MonoBehaviour {
    private const string RecruitCardView_Key = "RecruitCard_View";
    [SerializeField] private Transform recruitsContainer;
    [SerializeField] private Transform teamContainer;

    private void Start() {
        RefreshAll();
    }

    private void RefreshAll() {
        RefreshRecruitsUI();
        RefreshTeamUI();
    }
    private void RefreshRecruitsUI() {
        ClearRecruitsUI();
        var recruits = GameBootstrapper.Recruit.GetRecruits();
        foreach (var recruit in recruits) {
            var card = GameBootstrapper.ObjectPool.GetFromPool(RecruitCardView_Key);
            card.GetComponent<RecruitCard_View>().Setup(recruit,true);
            card.transform.SetParent(recruitsContainer, false);
        }
    }
    private void ClearRecruitsUI() {
        for (int i = recruitsContainer.childCount - 1; i >= 0; i--) {
            Transform child = recruitsContainer.GetChild(i);
            GameBootstrapper.ObjectPool.ReturnToPool(RecruitCardView_Key, child.gameObject);
        }
    }
    private void RefreshTeamUI() {
        ClearTeamUI();

        var team = GameBootstrapper.Recruit.GetTeam();
        foreach (var recruit in team) {
            var card = GameBootstrapper.ObjectPool.GetFromPool(RecruitCardView_Key);
            card.GetComponent<RecruitCard_View>().Setup(recruit);
            card.transform.SetParent(teamContainer, false);
        }
    }

    private void ClearTeamUI() {
        for (int i = teamContainer.childCount - 1; i >= 0; i--) {
            Transform child = teamContainer.GetChild(i);
            GameBootstrapper.ObjectPool.ReturnToPool(RecruitCardView_Key, child.gameObject);
        }
    }
    public void CloseOnClick() {
        GameBootstrapper.UI.CloseScreen("RecruitMenu");
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
