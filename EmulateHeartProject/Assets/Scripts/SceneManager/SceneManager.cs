using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private SceneFlowRouter sceneFlowRouter;
    [SerializeField]
    private MainCommonUIController mainCommonUIController;
    [SerializeField]
    private SubCommonUIManager subCommonUIManager;

    private SceneType _mainScene;
    private SceneType _subScene;
    private SceneType _activeScene;

    //Scene�ǉ����ҏW
    private readonly Dictionary<SceneType, SceneCategory> sceneCategoryMap = new()
    {
        { SceneType.None, SceneCategory.Sub },
        { SceneType.Title, SceneCategory.Main },
        { SceneType.Story, SceneCategory.Main },
        { SceneType.Chat, SceneCategory.Main },

        { SceneType.Load, SceneCategory.Sub },
        { SceneType.Settings, SceneCategory.Sub },
        { SceneType.Extra, SceneCategory.Sub },
        { SceneType.CharacterSelect, SceneCategory.Sub },
        { SceneType.Status, SceneCategory.Sub },
        { SceneType.Diary, SceneCategory.Sub },
        { SceneType.Save, SceneCategory.Sub },
    };

    private SceneCategory GetSceneCategory(SceneType sceneType)
    {
        if (sceneCategoryMap.TryGetValue(sceneType, out var category))
        {
            return category;
        }
        throw new ArgumentException($"Unknown scene type {sceneType}");
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        //�e�X�g
        _mainScene = SceneType.Title;
        _subScene = SceneType.None;
        //StartCoroutine(SkipAfterDelay(3f));
        //�e�X�g
        /*
        _mainScene = SceneType.Title;
        _subScene = SceneType.None;
        GoToScene(_mainScene);
        */
    }


    private IEnumerator SkipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GoToScene(_mainScene);
    }

    public void GameStart()
    {
        GoToScene(_mainScene);
    }


    public void GoToScene(SceneType sceneType)
    {
        if (sceneType == _activeScene) return;

        SetScene(sceneType);
    }

    public void CloseSubScene()
    {
        if (_subScene == SceneType.None) return;
        GoToScene(_mainScene);

    }

    public void ReopenScene()
    {
        var mainFlow = sceneFlowRouter.Get(_mainScene);
        var subFlow = sceneFlowRouter.Get(_subScene);

        mainFlow?.ReopenScene();
        subFlow?.ReopenScene();
    }

    private void SetScene(SceneType nextSceneType)
    {
        var category = GetSceneCategory(nextSceneType);

        if (category == SceneCategory.Sub)
            SwitchToSubScene(nextSceneType);
        else
            SwitchToMainScene(nextSceneType);

        _activeScene = nextSceneType;
    }
    private void SwitchToSubScene(SceneType subScene)
    {
        var subFlow = sceneFlowRouter.Get(subScene);
        var currentMainFlow = sceneFlowRouter.Get(_mainScene);

        _subScene = subScene;
        subCommonUIManager.SetCommonUI(subScene);
        mainCommonUIController.Close();
        subCommonUIManager.Open();


        currentMainFlow?.HideScene(); // ���C���͔�\���ɂ���
        subFlow.OpenScene();          // �T�u�͏����� + �\��
    }

    private void SwitchToMainScene(SceneType mainScene)
    {
        var newMainFlow = sceneFlowRouter.Get(mainScene);
        var oldMainFlow = sceneFlowRouter.Get(_mainScene);
        var oldSubFlow = sceneFlowRouter.Get(_subScene);

        if (mainScene == SceneType.Title)
        {
            mainCommonUIController.Close();
        }
        else
        {
            mainCommonUIController.Open();
        }

        if (_subScene != SceneType.None)
        {
            // �T�u�V�[������ă��C���ĕ\��
            oldSubFlow?.CloseScene();
            subCommonUIManager.Close();
            _subScene = SceneType.None;

            Debug.Log("Go" + mainScene.ToString());

            oldMainFlow?.ShowScene();
        }
        else
        {
            // ���݂̃��C������ĐV���C�����J��
            oldMainFlow?.CloseScene();
            Debug.Log("Go" + mainScene.ToString());

            newMainFlow.OpenScene();
            _mainScene = mainScene;
        }
    }
}
