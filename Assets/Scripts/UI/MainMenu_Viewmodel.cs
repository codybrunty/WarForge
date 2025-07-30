using UnityEngine;

public class MainMenu_Viewmodel : MonoBehaviour {

    public void RecruitOnClick() {
        GameBootstrapper.UI.OpenScreen("RecruitMenu");
    }

    public void ResetOnClick() {
        EventBus.Publish(new ResetGameEvent());
    }
}