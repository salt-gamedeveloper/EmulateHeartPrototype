using System.Collections.Generic;

public class CharacterSelectViewModel
{
    private Dictionary<CharacterType, bool> openingStatus;
    public Dictionary<CharacterType, bool> OpeningStaus => openingStatus;

    public CharacterSelectViewModel()
    {
        //ƒeƒXƒg
        openingStatus = new Dictionary<CharacterType, bool>();
        openingStatus.Add(CharacterType.Haru, true);
        openingStatus.Add(CharacterType.Natu, false);
        openingStatus.Add(CharacterType.Aki, false);
        openingStatus.Add(CharacterType.Fuyu, true);
    }
}
