using UnityEngine;
using TMPro;
public class Day_View : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI dayText;
    public void NextDayOnClick() {
        GameBootstrapper.Day.NextDay();
    }
    private void SetDayText(int day) {
        dayText.text = "Day " + day.ToString();
    }
    private void UpdateDayText(NewDayEvent evt) {
        SetDayText(evt.Day);
    }
    private void UpdateDayText(FirstDayEvent evt) {
        SetDayText(evt.Day);
    }
    private void OnEnable() {
        EventBus.Subscribe<FirstDayEvent>(UpdateDayText);
        EventBus.Subscribe<NewDayEvent>(UpdateDayText);
        SetDayText(GameBootstrapper.Day.CurrentDay);
    }
    private void OnDisable() {
        EventBus.Unsubscribe<FirstDayEvent>(UpdateDayText);
        EventBus.Unsubscribe<NewDayEvent>(UpdateDayText);
    }
}
