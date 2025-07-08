using UnityEngine;

//TODO: �Đ݌v
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
            //�e�X�g�p

            titleController.SubscribeToTest(() =>
            {
                Debug.Log("[SceneNavigationMediator] Test�{�^�� -> �V�[���֑J��");
                sceneManager.GoToScene(SceneType.CharacterSelect);
            });

            //�e�X�g�p

            titleController.SubscribeToLoadGame(() =>
            {
                Debug.Log("[SceneNavigationMediator] Load�{�^�� -> Load�V�[���֑J��");
                sceneManager.GoToScene(SceneType.Load);
            });
            titleController.SubscribeToExtra(() =>
            {
                Debug.Log("[SceneNavigationMediator] Extra�{�^�� -> Extra�V�[���֑J��");
                sceneManager.GoToScene(SceneType.Extra);
            });

            titleController.SubscribeToSetting(() =>
            {
                Debug.Log("[SceneNavigationMediator] Setting�{�^�� -> Settings�V�[���֑J��");
                sceneManager.GoToScene(SceneType.Settings);
            });

            Debug.Log("[SceneNavigationMediator] �{�^�����X�i�[��o�^�������܂���");
        }
        else
        {
            Debug.LogWarning("[SceneNavigationMediator] TitleSceneFlowController ���擾�ł��܂���ł���");
        }
    }


    private void SetupSubClose()
    {
        subCommonUIManager.SubscribeToSceneClose(() =>
        {
            Debug.Log("[SceneNavigationMediator] Close�{�^�� -> Sub�V�[���N���[�Y");
            sceneManager.CloseSubScene();
        });

        subCommonUIManager.SetupButtonCallbacks();
    }


    private void SetupSideMenu()
    {
        sideMenuView.SubscribeToLoadGame(() =>
        {
            //���[�h�V�[����
            sceneManager.GoToScene(SceneType.Load);
        });
        sideMenuView.SubscribeToStatus(() =>
        {
            //�X�e�[�^�X�V�[����
            sceneManager.GoToScene(SceneType.Status);
        });
        sideMenuView.SubscribeToDiary(() =>
        {
            //�_�C�����[�V�[����
            sceneManager.GoToScene(SceneType.Diary);
        });
        sideMenuView.SubscribeToSetting(() =>
        {
            //�ݒ�V�[����
            sceneManager.GoToScene(SceneType.Settings);
        });
    }

}