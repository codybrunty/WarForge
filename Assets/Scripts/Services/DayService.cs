using UnityEngine;

public class DayService{
    public int CurrentDay { get; private set; } = 1;
    private const string DayService_SaveLoadKey = "Day";
    public void FirstDay() {
        SaveDay();
        EventBus.Publish(new FirstDayEvent(CurrentDay));
    }
    public void NextDay() {
        CurrentDay++;
        SaveDay();
        EventBus.Publish(new NewDayEvent(CurrentDay));
    }

    public void SaveDay() {
        GameBootstrapper.SaveLoad.Save(DayService_SaveLoadKey, CurrentDay);
    }
    public void LoadDay() {
        if (!GameBootstrapper.SaveLoad.Exists(DayService_SaveLoadKey)) {
            CurrentDay = 1;
            FirstDay();
        }
        else {
            int dayData = GameBootstrapper.SaveLoad.Load<int>(DayService_SaveLoadKey);
            CurrentDay = dayData > 0 ? dayData : 1;
        }
    }
    public void DeleteDay() {
        GameBootstrapper.SaveLoad.Delete(DayService_SaveLoadKey);
    }
}
