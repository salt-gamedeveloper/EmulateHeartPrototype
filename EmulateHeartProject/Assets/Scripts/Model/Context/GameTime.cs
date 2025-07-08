
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
    /// ���Ԃ�1�i�߂�B���t���ς������ true ��Ԃ�
    /// </summary>
    public bool AdvanceTime()
    {
        // ���ԑт�1�i�߂�
        timeOfDay++;

        // Enum�͈̔͂𒴂��������i�߂Ē��ɖ߂�
        if ((int)timeOfDay > (int)TimeOfDay.Night)
        {
            dayCount++;
            timeOfDay = TimeOfDay.Morning;
            return true;
        }

        return false;
    }
}
