using UnityEngine;

public class LoadSceneFlowController : MonoBehaviour, ISceneFlowController
{
    SceneType sceneType = SceneType.Load;
    [SerializeField]
    private LoadSceneUIController controller;

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
