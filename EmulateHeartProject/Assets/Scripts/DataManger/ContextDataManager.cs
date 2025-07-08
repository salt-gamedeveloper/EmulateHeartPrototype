public class ContextDataManager
{
    private ContextData contextData;
    //test
    public void SetGameTime(GameTime gameTime)
    {
        contextData.SetGameTime(gameTime);
    }
    //test

    public ContextDataManager()
    {
        contextData = new ContextData();
    }

    public bool MoveLocation(Location move)
    {
        return contextData.MoveLocation(move);
    }

    public (TimeOfDay timeOfDay, bool isNewDay) AdvanceTime()
    {
        return contextData.AdvanceTime();
    }

    public GameTime GetGamaTime()
    {
        return contextData.GameTime;
    }

    public Location GetLocation()
    {
        return contextData.Location;
    }

    public ContextData GetContext()
    {
        return contextData;
    }

}
