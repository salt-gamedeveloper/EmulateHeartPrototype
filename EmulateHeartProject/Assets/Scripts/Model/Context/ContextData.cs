
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
        // ゲーム時間と場所の情報を結合してプロンプト文字列を作成します
        string contextText = $"現在の日付: Day {gameTime.DayCount}\n" +
                             $"現在の時間帯: {gameTime.TimeOfDay}\n" +
                             $"現在の場所: {location.LocationName} ({location.LocationType})";
        return contextText;
    }
}
