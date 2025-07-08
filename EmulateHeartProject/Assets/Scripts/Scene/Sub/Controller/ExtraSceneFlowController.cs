using UnityEngine;

public class ExtraSceneFlowController : MonoBehaviour, ISceneFlowController
{
    SceneType sceneType = SceneType.Extra;
    [SerializeField]
    private ExtraSceneUIController controller;

    public void Initialize()
    {
    }
    public void OpenScene()
    {
        controller.Open();
    }
    public void CloseScene()
    {
        controller.Close();
    }
    public void ShowScene()
    {
        controller.Show();
    }

    public void HideScene()
    {
        controller.Hide();
    }
    public void ReopenScene()
    {

    }

    public SceneType GetSceneType()
    {
        return sceneType;
    }
}
