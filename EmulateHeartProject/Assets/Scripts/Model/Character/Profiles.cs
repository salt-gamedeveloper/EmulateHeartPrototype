[System.Serializable]
public class Profiles
{
    public ProfileData aiProfile;
    public ProfileData playerProfile;

    public ProfileData AiProfile => aiProfile;
    public ProfileData PlayerProfile => playerProfile;

    //test
    public void debug()
    {
        aiProfile.debug();
        playerProfile.debug();
    }

    public void SetAiName(string aiName)
    {
        aiProfile.SetName(aiName);
    }
}
