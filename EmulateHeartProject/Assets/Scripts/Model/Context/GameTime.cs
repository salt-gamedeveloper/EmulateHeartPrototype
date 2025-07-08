
public class GameTime
{
    private int dayCount;
    private TimeOfDay timeOfDay;

    public int DayCount => dayCount;
    public TimeOfDay TimeOfDay => timeOfDay;

    public GameTime()
    {
        dayCount = 1;
        timeOfDay = TimeOfDay.Morning;
    }

    /// <summary>
    /// 時間を1つ進める。日付が変わったら true を返す
    /// </summary>
    public bool AdvanceTime()
    {
        // 時間帯を1つ進める
        timeOfDay++;

        // Enumの範囲を超えたら日を進めて朝に戻す
        if ((int)timeOfDay > (int)TimeOfDay.Night)
        {
            dayCount++;
            timeOfDay = TimeOfDay.Morning;
            return true;
        }

        return false;
    }
}
