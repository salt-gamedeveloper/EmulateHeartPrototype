using UnityEngine;

public class UIEmotionsField : MonoBehaviour
{
    [SerializeField]
    private RectTransform joy;
    [SerializeField]
    private RectTransform anger;
    [SerializeField]
    private RectTransform sorrow;
    [SerializeField]
    private RectTransform fun;

    private const float MAX_WIDTH = 235f;
    private const float INCREASE_AMOUNT = 10f;

    public void SceneReset()
    {
        Vector2 defaultSize = new Vector2(5f, 25f); // ���Z�b�g����f�t�H���g�T�C�Y���`

        // �eRectTransform�̃T�C�Y���f�t�H���g�l�ɐݒ�
        if (joy != null) joy.sizeDelta = defaultSize;
        if (anger != null) anger.sizeDelta = defaultSize;
        if (sorrow != null) sorrow.sizeDelta = defaultSize;
        if (fun != null) fun.sizeDelta = defaultSize;
    }

    public void AddEmotion(EmotionType emotion)
    {
        switch (emotion)
        {
            case EmotionType.Joy:
                if (joy != null) joy.sizeDelta = GetNewEmotionSize(joy.sizeDelta);
                break;
            case EmotionType.Anger:
                if (anger != null) anger.sizeDelta = GetNewEmotionSize(anger.sizeDelta);
                break;
            case EmotionType.Sorrow:
                if (sorrow != null) sorrow.sizeDelta = GetNewEmotionSize(sorrow.sizeDelta);
                break;
            case EmotionType.Fun:
                if (fun != null) fun.sizeDelta = GetNewEmotionSize(fun.sizeDelta);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ���݂̃T�C�Y�Ɋ�Â��āA�����ʂƍő�l���l�������V�����T�C�Y���v�Z���ĕԂ��܂��B
    /// </summary>
    /// <param name="currentSize">���݂�RectTransform��sizeDelta</param>
    /// <returns>�v�Z���ꂽ�V����sizeDelta</returns>
    private Vector2 GetNewEmotionSize(Vector2 currentSize)
    {
        float newX = Mathf.Min(currentSize.x + INCREASE_AMOUNT, MAX_WIDTH);
        return new Vector2(newX, currentSize.y);
    }
}
