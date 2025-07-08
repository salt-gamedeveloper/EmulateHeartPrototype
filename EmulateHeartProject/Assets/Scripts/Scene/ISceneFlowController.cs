public interface ISceneFlowController
{
    void Initialize();
    void OpenScene();
    void CloseScene();
    void ShowScene();
    void HideScene();
    void ReopenScene();

    SceneType GetSceneType();
}
