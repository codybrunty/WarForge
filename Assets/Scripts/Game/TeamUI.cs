using UnityEngine;

public class TeamUI : MonoBehaviour {
    
    private const string RecruitCardUIPoolKey = "RecruitCardUI";
    [SerializeField] private Transform teamContainer;

    private void Start() {
        Refresh();
    }

    public void Refresh() {
        Clear();

        GameBootstrapper.RecruitService.RefreshRecruitPool();
        var team = GameBootstrapper.RecruitService.GetRecruitPool();
        foreach (var recruit in team) {
            var card = GameBootstrapper.ObjectPool.GetFromPool(RecruitCardUIPoolKey);
            card.GetComponent<RecruitCardUI>().Setup(recruit);
            card.transform.SetParent(teamContainer, false);
        }
    }

    private void Clear() {
        foreach (Transform child in teamContainer) {
            GameBootstrapper.ObjectPool.ReturnToPool(RecruitCardUIPoolKey, child.gameObject);
        }
    }
}
