using UnityEngine;

public class SettingsSceneFlowController : MonoBehaviour, ISceneFlowController
{
    SceneType sceneType = SceneType.Settings;
    [SerializeField]
    private SettingsSceneUIController settingsSceneUIController;

    public void Initialize()
    {
    }
    public void OpenScene()
    {
        settingsSceneUIController.Open();
    }
    public void CloseScene()
    {
        settingsSceneUIController.Close();
    }
    public void ShowScene()
    {
        settingsSceneUIController.Show();
    }

    public void HideScene()
    {
        settingsSceneUIController.Hide();
    }
    public void ReopenScene()
    {

    }

    public SceneType GetSceneType()
    {
        return sceneType;
    }
}
