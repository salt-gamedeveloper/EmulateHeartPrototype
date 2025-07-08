using System.Collections;
using UnityEngine;

public class UIImageChanger : MonoBehaviour
{
    [SerializeField] private UIImageView imageView;
    [SerializeField] private ImagesSO imagesSO;

    // �e�X�g�p
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
    //�e�X�g�p

    public void SetImage(string fileId)
    {
        var imageData = imagesSO?.GetById(fileId);
        if (imageData != null)
        {
            imageView.SetSprite(imageData.Sprite);
        }
        else
        {
            Debug.LogWarning($"[UIImageChanger] �w�肳�ꂽID�̉摜��������܂���: {fileId}");
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
            Debug.LogWarning($"[UIImageChanger] �w�肳�ꂽID�̉摜��������܂���: {fileId}");
            return;
        }

        // �����\�����Ȃ�t�F�[�h�A�E�g �� �ύX �� �t�F�[�h�C��
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
            // ��\����ԂȂ璼�ډ摜�ύX���ĕ\����������
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
            Debug.LogWarning($"[UIImageChanger] �w�肳�ꂽID�̉摜��������܂���: {fileId}");
            return;
        }

        imageView.SetSprite(imageData.Sprite);   // �X�v���C�g�������f

        imageView.ShowImmediate();
    }

    public void ShowWithCallback(string fileId, System.Action onComplete)
    {
        var imageData = imagesSO?.GetById(fileId);
        if (imageData == null)
        {
            Debug.LogWarning($"[UIImageChanger] �w�肳�ꂽID�̉摜��������܂���: {fileId}");
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
