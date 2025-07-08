using System.Collections;
using UnityEngine;

public class UIImageChanger : MonoBehaviour
{
    [SerializeField] private UIImageView imageView;
    [SerializeField] private ImagesSO imagesSO;

    // テスト用
    /*
    void Start()
    {
        
        SetImage("ai_angry");
        imageView.Show();
        imageView.SetColor(ColorType.Gray221A80);
        
        imageView.Hide();
        //ChangeImage("Ch1_2");
        StartCoroutine(SkipAfterDelay(3f));
    }

    private IEnumerator SkipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeImage("airoom_afternoon");
    }
    */
    //テスト用

    public void SetImage(string fileId)
    {
        var imageData = imagesSO?.GetById(fileId);
        if (imageData != null)
        {
            imageView.SetSprite(imageData.Sprite);
        }
        else
        {
            Debug.LogWarning($"[UIImageChanger] 指定されたIDの画像が見つかりません: {fileId}");
        }
    }

    public void SetColor(ColorType colorType)
    {
        imageView.SetColor(colorType);
    }

    public void ChangeImage(string fileId, System.Action onComplete = null, float delayAfterFadeOut = 0f)
    {
        var imageData = imagesSO?.GetById(fileId);
        if (imageData == null)
        {
            Debug.LogWarning($"[UIImageChanger] 指定されたIDの画像が見つかりません: {fileId}");
            return;
        }

        // もし表示中ならフェードアウト → 変更 → フェードイン
        if (imageView.IsVisible())
        {
            imageView.HideWithCallback(() =>
            {
                if (delayAfterFadeOut > 0f)
                {
                    StartCoroutine(DelayedChange(imageData.Sprite, onComplete, delayAfterFadeOut));
                }
                else
                {
                    imageView.SetSprite(imageData.Sprite);
                    imageView.ShowWithCallback(() => onComplete?.Invoke());
                }
            });
        }
        else
        {
            // 非表示状態なら直接画像変更して表示だけする
            imageView.SetSprite(imageData.Sprite);
            imageView.ShowWithCallback(() => onComplete?.Invoke());
        }
    }

    private IEnumerator DelayedChange(Sprite sprite, System.Action onComplete, float delay)
    {
        yield return new WaitForSeconds(delay);
        imageView.SetSprite(sprite);
        imageView.ShowWithCallback(() => onComplete?.Invoke());
    }

    public void ChangeImageImmediate(string fileId)
    {
        var imageData = imagesSO?.GetById(fileId);
        if (imageData == null)
        {
            Debug.LogWarning($"[UIImageChanger] 指定されたIDの画像が見つかりません: {fileId}");
            return;
        }

        imageView.SetSprite(imageData.Sprite);   // スプライト即座反映

        imageView.ShowImmediate();
    }

    public void ShowWithCallback(string fileId, System.Action onComplete)
    {
        var imageData = imagesSO?.GetById(fileId);
        if (imageData == null)
        {
            Debug.LogWarning($"[UIImageChanger] 指定されたIDの画像が見つかりません: {fileId}");
            return;
        }
        imageView.SetSprite(imageData.Sprite);
        imageView.ShowWithCallback(() => onComplete?.Invoke());
    }

    public void HideImmediate()
    {
        //Debug.Log("Hide");
        imageView.HideImmediate();
    }

    public void Clear()
    {
        imageView.SetSprite(null);
    }
}
