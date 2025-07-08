using DG.Tweening;
using UnityEngine;


public class UIAccordionButtonView : MonoBehaviour
{
    [SerializeField]
    private UIButtonView buttonView;
    [SerializeField]
    private UIImageChanger arrowImageChanger;
    [SerializeField]
    private RectTransform contentTransform;

    private float height;
    private bool isVisible;

    // テスト用
    /*
    void Start()
    {
        StartCoroutine(SkipAfterDelay(5f));
    }

    private IEnumerator SkipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Show();
        StartCoroutine(SkipAfterDelay2(4f));
    }
    private IEnumerator SkipAfterDelay2(float delay)
    {

        yield return new WaitForSeconds(delay);
        Hide();
    }
    */
    //テスト用

    private void Awake()
    {
        height = contentTransform.rect.height;

        contentTransform.anchoredPosition = new Vector2(0, height); // 非表示位置
        arrowImageChanger.ChangeImageImmediate("arrow_down");
        isVisible = false;

        buttonView.SubscribeToButtonClick(OnButton);
    }

    public void Show()
    {
        contentTransform.DOAnchorPosY(0f, 0.5f)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                arrowImageChanger.ChangeImageImmediate("arrow_up");
            });
        isVisible = true;
    }

    public void Hide()
    {
        contentTransform.DOAnchorPosY(height, 0.5f)
            .SetEase(Ease.InCubic)
            .OnComplete(() =>
            {
                arrowImageChanger.ChangeImageImmediate("arrow_down");
            });
        isVisible = false;
    }

    public void OnButton()
    {
        if (isVisible)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

}
