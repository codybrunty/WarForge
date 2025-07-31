public class RecruitSelectedEvent {
    public Recruit Recruit;
    public RecruitSelectedEvent(Recruit recruit) {
        Recruit = recruit;
    }
}

public class ResetGameEvent {}

public class NewDayEvent {
    public int Day;
    public NewDayEvent(int day) {
        Day = day;
    }
}
public class FirstDayEvent {
    public int Day;
    public FirstDayEvent(int day) {
        Day = day;
    }
}