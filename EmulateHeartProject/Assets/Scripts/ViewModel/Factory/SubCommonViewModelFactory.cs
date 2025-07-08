using UnityEngine;

public class SubCommonViewModelFactory
{
    public SubCoommonViewModel Create(SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneType.Title:
                return new SubCoommonViewModel("Title", "icon_title");
            case SceneType.Load:
                return new SubCoommonViewModel("Load", "icon_load");

            case SceneType.Settings:
                return new SubCoommonViewModel("Setting", "icon_settings");

            case SceneType.Extra:
                return new SubCoommonViewModel("Extra", "icon_extra");
            case SceneType.CharacterSelect:
                return new SubCoommonViewModel("CharacterSelect", "");
            case SceneType.Status:
                return new SubCoommonViewModel("Status", "");
            case SceneType.Diary:
                return new SubCoommonViewModel("Diary", "");
            case SceneType.Save:
                return new SubCoommonViewModel("Save", "");
            default:
                Debug.LogWarning($"Unknown SceneType: {sceneType}, returning default ViewModel.");
                return new SubCoommonViewModel("•s–¾", "icon_default");
        }
    }
}
