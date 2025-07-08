public class ChatContextViewModel
{
    private LocationType location;
    private TimeOfDay time;
    private int day;

    public LocationType Location => location;
    public TimeOfDay Time => time;
    public int Day => day;

    //テスト
    public ChatContextViewModel(LocationType location, TimeOfDay timeOfDay, int day)
    {
        this.location = location;
        this.time = timeOfDay;
        this.day = day;
    }
    //テスト
}
