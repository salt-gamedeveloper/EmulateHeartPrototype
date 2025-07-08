using UnityEngine;

//TODO: �S�̓I�Ȑ݌v�̌������BDialog��enum��SO�ł����Ƃ��ꂢ�ɊǗ�����
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
                Debug.Log("Title�V�[����Exit�w��");
                // TODO : SO��ID�w��ɂ���
                SmallDialogViewModel viewModel = new SmallDialogViewModel();
                string yesText = "�Q�[���I��";
                string noText = "������";
                string dialogText = "�Q�[�����I�����܂����H";
                viewModel.CreateYesNoDialogViewModel(yesText, noText, dialogText);
                viewModel.SubscribeToYes(() => Application.Quit());
                dialogManager.OpenSmallDialog(viewModel);

            });
            titleController.SubscribeToApiKeyInfo(() =>
            {
                Debug.Log("Title�V�[����Info�w��");
                // TODO : SO��ID�w��ɂ���
                SmallDialogViewModel viewModel = new SmallDialogViewModel();
                string yesText = "APIKey���擾����";
                string noText = "�߂�";
                string dialogText = "Gemini��APIKey��\n�K�v�ł�";
                viewModel.CreateYesNoDialogViewModel(yesText, noText, dialogText);
                viewModel.SubscribeToYes(() => Application.OpenURL("https://aistudio.google.com/app/apikey"));
                dialogManager.OpenSmallDialog(viewModel);

            });
            Debug.Log("Title�V�[��Dialog�Z�b�g�A�b�v����");
        }
        else
        {
            Debug.LogWarning("[DialogMediator] TitleSceneFlowController ���擾�ł��܂���ł���");
        }
    }

    private void SetupChatSceneFlow()
    {
        if (sceneFlowRouter.Get(SceneType.Chat) is ChatSceneFlowController controller)
        {
            controller.SubscribeToChatLog(() =>
            {
                Debug.Log("Chat�V�[����ChatLog�w��");
                dialogManager.OpenChatToLog();

            });
            controller.SubscribeToGoToTitle(() =>
            {
                SmallDialogViewModel viewModel = new SmallDialogViewModel();
                string closeText = "�^�C�g����ʂ�";
                string dialogText = "�v���g�^�C�v�͂����܂�\n1���̃��U���g�͊J����\n�^�C�g����ʂɖ߂�܂�";
                viewModel.CreateInfoDialogViewModel(closeText, dialogText);
                viewModel.SubscribeToClose(() => sceneManager.GoToScene(SceneType.Title));
                dialogManager.OpenSmallDialog(viewModel);

            });
            Debug.Log("Chat�V�[��Dialog�Z�b�g�A�b�v����");
        }
        else
        {
            Debug.LogWarning("[DialogMediator] ChatSceneFlowController ���擾�ł��܂���ł���");
        }
    }

    private void SetupCharacterSelectFlow()
    {
        if (sceneFlowRouter.Get(SceneType.CharacterSelect) is CharacterSelectSceneFlowController controller)
        {
            controller.SubscribeToConfirm((characterType, aiNameFromController) =>
            {
                Debug.Log("Confirm�w��");
                string selectedCharacterName = Enums.ConvertCharacterType(characterType);
                string aiName = aiNameFromController;

                // TODO : SO��ID�w��ɂ���
                SmallDialogViewModel viewModel = new SmallDialogViewModel();
                string yesText = "����";
                string noText = "�L�����Z��";
                string dialogText = "���f�� : " + selectedCharacterName + "\n" + aiName + "\n����Ŏn�߂܂����H";
                viewModel.CreateYesNoDialogViewModel(yesText, noText, dialogText);
                viewModel.SubscribeToYes(() =>
                {
                    sceneManager.CloseSubScene();
                    sceneManager.GoToScene(SceneType.Chat);
                    DataFacade.Instance.Character.CharacterSelect(characterType, aiName);
                });
                dialogManager.OpenSmallDialog(viewModel);

            });

            Debug.Log("Title�V�[��Dialog�Z�b�g�A�b�v����");
        }
        else
        {
            Debug.LogWarning("[DialogMediator] TitleSceneFlowController ���擾�ł��܂���ł���");
        }
    }

    private void SetupSideMenu()
    {

        sideMenuFieldView.SubscribeToTitle(() =>
        {
            Debug.Log("�^�C�g���ڍs���O");
            SmallDialogViewModel viewModel = new SmallDialogViewModel();
            string yesText = "�^�C�g���֖߂�";
            string noText = "������";
            string dialogText = "�Z�[�u������\n�^�C�g���ɖ߂�܂����H";
            viewModel.CreateYesNoDialogViewModel(yesText, noText, dialogText);
            viewModel.SubscribeToYes(() => sceneManager.GoToScene(SceneType.Title));
            dialogManager.OpenSmallDialog(viewModel);
        });
    }
}
