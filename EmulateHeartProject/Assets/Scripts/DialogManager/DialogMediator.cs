using UnityEngine;

//TODO: 全体的な設計の見直し。DialogはenumとSOでもっときれいに管理する
public class DialogMediator : MonoBehaviour
{
    [SerializeField]
    private SceneFlowRouter sceneFlowRouter;
    [SerializeField]
    private MainCommonUIController mainCommonUIController;

    [SerializeField]
    private UISideMenuFieldView sideMenuFieldView;

    [SerializeField]
    private DialogManager dialogManager;
    [SerializeField]
    private SceneManager sceneManager;

    private void Start()
    {
        SetupTitleSceneFlow();
        SetupMainCommonUI();
        SetupChatSceneFlow();
        SetupCharacterSelectFlow();
        SetupSideMenu();
    }
    private void SetupMainCommonUI()
    {
        mainCommonUIController.SubscribeToSideMenu(() =>
        {
            dialogManager.OpenSideMenu();
        });
    }

    private void SetupTitleSceneFlow()
    {
        if (sceneFlowRouter.Get(SceneType.Title) is TitleSceneFlowController titleController)
        {
            titleController.SubscribeToExit(() =>
            {
                Debug.Log("TitleシーンのExit購読");
                // TODO : SOのID指定にする
                SmallDialogViewModel viewModel = new SmallDialogViewModel();
                string yesText = "ゲーム終了";
                string noText = "続ける";
                string dialogText = "ゲームを終了しますか？";
                viewModel.CreateYesNoDialogViewModel(yesText, noText, dialogText);
                viewModel.SubscribeToYes(() => Application.Quit());
                dialogManager.OpenSmallDialog(viewModel);

            });
            titleController.SubscribeToApiKeyInfo(() =>
            {
                Debug.Log("TitleシーンのInfo購読");
                // TODO : SOのID指定にする
                SmallDialogViewModel viewModel = new SmallDialogViewModel();
                string yesText = "APIKeyを取得する";
                string noText = "戻る";
                string dialogText = "GeminiのAPIKeyが\n必要です";
                viewModel.CreateYesNoDialogViewModel(yesText, noText, dialogText);
                viewModel.SubscribeToYes(() => Application.OpenURL("https://aistudio.google.com/app/apikey"));
                dialogManager.OpenSmallDialog(viewModel);

            });
            Debug.Log("TitleシーンDialogセットアップ完了");
        }
        else
        {
            Debug.LogWarning("[DialogMediator] TitleSceneFlowController が取得できませんでした");
        }
    }

    private void SetupChatSceneFlow()
    {
        if (sceneFlowRouter.Get(SceneType.Chat) is ChatSceneFlowController controller)
        {
            controller.SubscribeToChatLog(() =>
            {
                Debug.Log("ChatシーンのChatLog購読");
                dialogManager.OpenChatToLog();

            });
            controller.SubscribeToGoToTitle(() =>
            {
                SmallDialogViewModel viewModel = new SmallDialogViewModel();
                string closeText = "タイトル画面へ";
                string dialogText = "プロトタイプはここまで\n1日のリザルトは開発中\nタイトル画面に戻ります";
                viewModel.CreateInfoDialogViewModel(closeText, dialogText);
                viewModel.SubscribeToClose(() => sceneManager.GoToScene(SceneType.Title));
                dialogManager.OpenSmallDialog(viewModel);

            });
            Debug.Log("ChatシーンDialogセットアップ完了");
        }
        else
        {
            Debug.LogWarning("[DialogMediator] ChatSceneFlowController が取得できませんでした");
        }
    }

    private void SetupCharacterSelectFlow()
    {
        if (sceneFlowRouter.Get(SceneType.CharacterSelect) is CharacterSelectSceneFlowController controller)
        {
            controller.SubscribeToConfirm((characterType, aiNameFromController) =>
            {
                Debug.Log("Confirm購読");
                string selectedCharacterName = Enums.ConvertCharacterType(characterType);
                string aiName = aiNameFromController;

                // TODO : SOのID指定にする
                SmallDialogViewModel viewModel = new SmallDialogViewModel();
                string yesText = "決定";
                string noText = "キャンセル";
                string dialogText = "モデル : " + selectedCharacterName + "\n" + aiName + "\nこれで始めますか？";
                viewModel.CreateYesNoDialogViewModel(yesText, noText, dialogText);
                viewModel.SubscribeToYes(() =>
                {
                    sceneManager.CloseSubScene();
                    sceneManager.GoToScene(SceneType.Chat);
                    DataFacade.Instance.Character.CharacterSelect(characterType, aiName);
                });
                dialogManager.OpenSmallDialog(viewModel);

            });

            Debug.Log("TitleシーンDialogセットアップ完了");
        }
        else
        {
            Debug.LogWarning("[DialogMediator] TitleSceneFlowController が取得できませんでした");
        }
    }

    private void SetupSideMenu()
    {

        sideMenuFieldView.SubscribeToTitle(() =>
        {
            Debug.Log("タイトル移行ログ");
            SmallDialogViewModel viewModel = new SmallDialogViewModel();
            string yesText = "タイトルへ戻る";
            string noText = "続ける";
            string dialogText = "セーブせずに\nタイトルに戻りますか？";
            viewModel.CreateYesNoDialogViewModel(yesText, noText, dialogText);
            viewModel.SubscribeToYes(() => sceneManager.GoToScene(SceneType.Title));
            dialogManager.OpenSmallDialog(viewModel);
        });
    }
}
