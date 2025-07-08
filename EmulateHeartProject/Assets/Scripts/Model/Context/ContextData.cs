
public class ContextData
{
    private GameTime gameTime;
    private Location location;

    public GameTime GameTime => gameTime;
    public Location Location => location;

    //test
    public void SetGameTime(GameTime gameTime)
    {
        this.gameTime = gameTime;
    }
    //test

    public ContextData()
    {
        gameTime = new GameTime();
        location = new Location();
    }

    public bool MoveLocation(Location move)
    {
        if (move == null || location == move) return false;
        location = move;
        return true;
    }

    public (TimeOfDay timeOfDay, bool isNewDay) AdvanceTime()
    {
        bool isNewDay = gameTime.AdvanceTime();
        return (gameTime.TimeOfDay, isNewDay);
    }

    public string ToPromptString()
    {
        // �Q�[�����ԂƏꏊ�̏����������ăv�����v�g��������쐬���܂�
        string contextText = $"���݂̓��t: Day {gameTime.DayCount}\n" +
                             $"���݂̎��ԑ�: {gameTime.TimeOfDay}\n" +
                             $"���݂̏ꏊ: {location.LocationName} ({location.LocationType})";
        return contextText;
    }
}
