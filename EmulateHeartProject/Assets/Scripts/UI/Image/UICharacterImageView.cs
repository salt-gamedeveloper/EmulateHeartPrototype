using UnityEngine;

public class UICharacterImageView : MonoBehaviour
{
    [SerializeField]
    private UIImageChanger changer;

    private CharacterType characterType;
    private CharacterExpression expression;

    // �e�X�g�p
    /*
    void Start()
    {
        StartCoroutine(SkipAfterDelay(5f));
    }

    private IEnumerator SkipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeCharacter(CharacterType.Ai, CharacterExpression.Cry);
        StartCoroutine(SkipAfterDelay2(4f));
    }
    private IEnumerator SkipAfterDelay2(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
    */
    //�e�X�g�p

    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        //SceneReset();
    }
    public void StartScene(System.Action onComplete)
    {
        expression = CharacterExpression.Neutral;
        string fileId = FileIdGenerator.GetCharacterExpressionId(characterType, expression);
        changer.ChangeImage(fileId, onComplete);
        //Debug.Log("start�����G�؂�ւ�");
    }

    public void SceneReset()
    {
        //Debug.Log("Reset�����G�؂�ւ�");
        changer.HideImmediate();
        characterType = DataFacade.Instance.Character.PlayCharacter;
    }

    public void SetCharacterType(CharacterType character)
    {
        this.characterType = character;
    }

    public void ChangeCharacter(CharacterType newCharacter, CharacterExpression newExpression = CharacterExpression.Neutral, ColorType newColor = ColorType.A100, System.Action onComplete = null)
    {

        //Debug.Log("�����G�؂�ւ�");
        bool isSameCharacter = characterType == newCharacter;
        bool isSameExpression = expression == newExpression;

        if (isSameCharacter && isSameExpression)
        {
            onComplete?.Invoke();
            return; // �L�������\��������Ȃ牽�����Ȃ�
        }

        changer.SetColor(newColor);
        characterType = newCharacter;
        expression = newExpression;
        string fileId = FileIdGenerator.GetCharacterExpressionId(characterType, expression);

        if (isSameCharacter && !isSameExpression)
        {
            changer.ChangeImageImmediate(fileId); // �����؂�ւ�
            onComplete?.Invoke();
        }
        else
        {
            changer.ChangeImage(fileId, onComplete); // �A�j���[�V�����t���؂�ւ�
        }
    }

    public void SetShadow(CharacterType newCharacter, ColorType colorType = ColorType.Default, System.Action onComplete = null)
    {
        if (colorType == ColorType.Default)
        {
            colorType = ColorTypeMapper.GetColorType(newCharacter, ColorPurpose.Silhouette);
        }
        ChangeCharacter(newCharacter, CharacterExpression.Shd, colorType, onComplete);
    }
}
