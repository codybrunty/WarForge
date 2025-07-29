using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecruitCardUI : MonoBehaviour {
    [SerializeField] private Image portraitImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI classText;

    public void Setup(Recruit recruit) {
        nameText.text = recruit.Name;
        classText.text = recruit.ClassType.ToString();
        portraitImage.sprite = GetPortraitForClass(recruit.ClassType);
    }

    private Sprite GetPortraitForClass(RecruitType recruitType) {
        return GameConfigs.SpriteConfig.Get($"{recruitType}_Portrait");
    }
}
