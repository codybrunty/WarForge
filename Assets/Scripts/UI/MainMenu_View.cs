using UnityEngine;

public class MainMenu_View : MonoBehaviour {

    public void RecruitOnClick() {
        GameBootstrapper.UI.OpenScreen("RecruitMenu");
    }

    public void ResetOnClick() {
        EventBus.Publish(new ResetGameEvent());
    }
}