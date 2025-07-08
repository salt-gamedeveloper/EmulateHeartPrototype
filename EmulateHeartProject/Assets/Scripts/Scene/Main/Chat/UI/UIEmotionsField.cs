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
        Vector2 defaultSize = new Vector2(5f, 25f); // リセットするデフォルトサイズを定義

        // 各RectTransformのサイズをデフォルト値に設定
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
    /// 現在のサイズに基づいて、増加量と最大値を考慮した新しいサイズを計算して返します。
    /// </summary>
    /// <param name="currentSize">現在のRectTransformのsizeDelta</param>
    /// <returns>計算された新しいsizeDelta</returns>
    private Vector2 GetNewEmotionSize(Vector2 currentSize)
    {
        float newX = Mathf.Min(currentSize.x + INCREASE_AMOUNT, MAX_WIDTH);
        return new Vector2(newX, currentSize.y);
    }
}
