using UnityEngine;

//TODO: 再設計
public class SceneNavigationMediator : MonoBehaviour
{
    [SerializeField]
    private SceneFlowRouter sceneFlowRouter;
    [SerializeField]
    private SubCommonUIManager subCommonUIManager;
    [SerializeField]
    private UISideMenuFieldView sideMenuView;
    [SerializeField]
    private OpeningController openingController;

    [SerializeField]
    private SceneManager sceneManager;

    private void Start()
    {
        SetupOpening();
        SetupTitleSceneFlow();
        SetupSubClose();
        SetupSideMenu();
    }


    private void SetupOpening()
    {
        openingController.SubscribeToGameStart(() => sceneManager.GameStart());
    }

    private void SetupTitleSceneFlow()
    {
        if (sceneFlowRouter.Get(SceneType.Title) is TitleSceneFlowController titleController)
        {
            //テスト用

            titleController.SubscribeToTest(() =>
            {
                Debug.Log("[SceneNavigationMediator] Testボタン -> シーンへ遷移");
                sceneManager.GoToScene(SceneType.CharacterSelect);
            });

            //テスト用

            titleController.SubscribeToLoadGame(() =>
            {
                Debug.Log("[SceneNavigationMediator] Loadボタン -> Loadシーンへ遷移");
                sceneManager.GoToScene(SceneType.Load);
            });
            titleController.SubscribeToExtra(() =>
            {
                Debug.Log("[SceneNavigationMediator] Extraボタン -> Extraシーンへ遷移");
                sceneManager.GoToScene(SceneType.Extra);
            });

            titleController.SubscribeToSetting(() =>
            {
                Debug.Log("[SceneNavigationMediator] Settingボタン -> Settingsシーンへ遷移");
                sceneManager.GoToScene(SceneType.Settings);
            });

            Debug.Log("[SceneNavigationMediator] ボタンリスナーを登録完了しました");
        }
        else
        {
            Debug.LogWarning("[SceneNavigationMediator] TitleSceneFlowController が取得できませんでした");
        }
    }


    private void SetupSubClose()
    {
        subCommonUIManager.SubscribeToSceneClose(() =>
        {
            Debug.Log("[SceneNavigationMediator] Closeボタン -> Subシーンクローズ");
            sceneManager.CloseSubScene();
        });

        subCommonUIManager.SetupButtonCallbacks();
    }


    private void SetupSideMenu()
    {
        sideMenuView.SubscribeToLoadGame(() =>
        {
            //ロードシーンへ
            sceneManager.GoToScene(SceneType.Load);
        });
        sideMenuView.SubscribeToStatus(() =>
        {
            //ステータスシーンへ
            sceneManager.GoToScene(SceneType.Status);
        });
        sideMenuView.SubscribeToDiary(() =>
        {
            //ダイヤリーシーンへ
            sceneManager.GoToScene(SceneType.Diary);
        });
        sideMenuView.SubscribeToSetting(() =>
        {
            //設定シーンへ
            sceneManager.GoToScene(SceneType.Settings);
        });
    }

}