using System;
using UnityEngine;
using UnityEngine.UI;

public class RecruitCardUI_Recruit : RecruitCardUI {
    public void RecruitOnClick() {
        EventBus.Publish(new RecruitSelectedEvent(recruit));
    }
}

