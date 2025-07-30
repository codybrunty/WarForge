using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecruitCard_View : MonoBehaviour {
    [SerializeField] private Image portraitImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI classText;
    [SerializeField] private Color boyColor;
    [SerializeField] private Color girlColor;
    [SerializeField] private GameObject recruitButton;
    protected Recruit recruit;
    public void Setup(Recruit recruit,bool enableRecruitButton=false) {
        this.recruit = recruit;
        nameText.text = recruit.Name;
        classText.text = recruit.ClassType.ToString();
        portraitImage.sprite = GetPortraitForClass(recruit.ClassType);
        SetGenderColor(recruit.Gender == RecruitGender.Male ? boyColor : girlColor);
        recruitButton.SetActive(enableRecruitButton);
    }

    private void SetGenderColor(Color color) {
        portraitImage.color = color;
        classText.color = color;
        nameText.color = color;
    }

    private Sprite GetPortraitForClass(RecruitType recruitType) {
        return GameConfigs.SpriteConfig.Get($"{recruitType}_Portrait");
    }
    public void RecruitOnClick() {
        EventBus.Publish(new RecruitSelectedEvent(recruit));
    }
}

