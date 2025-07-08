using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectSceneUIController : UIControllerBase
{
    [SerializeField]
    private UICharacterImageView characterImageView;
    [SerializeField]
    private UICharacterImageView characterShadow;
    [SerializeField]
    private UICharacterSelectAiSettingFieldView aiSettingFieldView;
    [SerializeField]
    private UITabView tabView;

    private CharacterType selectCharacter;

    private Dictionary<CharacterType, bool> openingStatus;
    System.Action<CharacterType, string> OnConfirmAction;


    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => ChangeCharacter(CharacterType.Haru));
        actions.Add(() => ChangeCharacter(CharacterType.Natu));
        actions.Add(() => ChangeCharacter(CharacterType.Aki));
        actions.Add(() => ChangeCharacter(CharacterType.Fuyu));
        tabView.SubscribeToTabClick(actions);
        aiSettingFieldView.SubscribeToConfirm(ConfirmCharacter);
        Close();
    }

    public void SceneReset()
    {
        aiSettingFieldView.SceneReset();
        characterImageView.SceneReset();
        characterShadow.SceneReset();
        selectCharacter = CharacterType.Haru;
    }

    public void Open()
    {
        Show();
    }
    public void Close()
    {
        Hide();
    }

    public void StartScene(Dictionary<CharacterType, bool> openingStatus, System.Action onComplete)
    {
        this.openingStatus = openingStatus;
        characterImageView.SetCharacterType(selectCharacter);
        characterShadow.SetCharacterType(selectCharacter);
        characterShadow.StartScene(null);
        characterImageView.StartScene(() =>
        {
            ChangeCharacter(CharacterType.Haru);
            aiSettingFieldView.Show();
            onComplete?.Invoke();
        });
    }

    private void ChangeCharacter(CharacterType character)
    {
        SetInteractable(false);
        selectCharacter = character;
        aiSettingFieldView.SetCharacterType(selectCharacter);
        bool status = openingStatus[selectCharacter];
        string statusId;
        if (status)
        {
            statusId = "public";
            characterImageView.ChangeCharacter(selectCharacter);
        }
        else
        {
            statusId = "private";
            characterImageView.SetShadow(selectCharacter, ColorType.Black95);
        }
        aiSettingFieldView.SetInteractable(status);

        string fileId = Enums.CharacterTypeToLowerString(selectCharacter) + "_" + statusId;
        tabView.SetImage(fileId);
        characterShadow.SetShadow(selectCharacter, ColorType.Default, () =>
        {
            SetInteractable(true);
        });
    }

    private void ConfirmCharacter(string aiName)
    {
        OnConfirmAction?.Invoke(selectCharacter, aiName);
        Debug.Log("ConfirmåƒÇ—èoÇµ");
    }

    public void SubscribeToConfirm(System.Action<CharacterType, string> listener)
    {
        OnConfirmAction = listener;

    }
}
