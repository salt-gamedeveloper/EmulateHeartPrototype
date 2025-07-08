using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: キャラクター選択後、新規ゲーム開始の流れを管理するクラスへ
public class CharacterSelectSceneFlowController : MonoBehaviour, ISceneFlowController
{
    SceneType sceneType = SceneType.CharacterSelect;
    [SerializeField]
    private CharacterSelectSceneUIController controller;

    private Dictionary<CharacterType, bool> openingStatus;

    private float scenarioDelay;

    private CharacterType selectCharacter;
    private string aiName;

    System.Action<CharacterType, string> OnConfirmAction;


    public void Initialize()
    {
        scenarioDelay = 0.5f;
        CloseScene();
    }
    public void OpenScene()
    {
        controller.SceneReset();
        //TODO: 適切なViewModel取得

        //テスト
        CharacterSelectViewModel viewModel = new CharacterSelectViewModel();
        //テスト

        openingStatus = viewModel.OpeningStaus;

        controller.Open();
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(scenarioDelay);
        controller.StartScene(openingStatus, null);
    }
    public void CloseScene()
    {
        controller.Close();
        controller.SceneReset();
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

    public CharacterType GetSelectCharacter()
    {
        return selectCharacter;
    }

    public string GetAiName()
    {
        return aiName;
    }

    public void SubscribeToConfirm(System.Action<CharacterType, string> listener)
    {
        OnConfirmAction = listener;
        controller.SubscribeToConfirm(OnConfirmAction);
    }
}
