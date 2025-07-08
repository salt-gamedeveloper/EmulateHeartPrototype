using UnityEngine;

public class UIBGImageView : MonoBehaviour
{
    [SerializeField]
    private UIImageChanger changer;

    string fileId;

    // テスト用
    /*
    void Start()
    {
        StartCoroutine(SkipAfterDelay(5f));
    }

    private IEnumerator SkipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeBg(LocationType.Living,TimeOfDay.Afternoon);
        StartCoroutine(SkipAfterDelay2(4f));
    }
    private IEnumerator SkipAfterDelay2(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
    */
    //テスト用
    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        SceneReset();
    }
    public void SceneReset()
    {
        changer.HideImmediate();
    }

    //無条件で黒から画像表示
    public void StartScene(LocationType newLocation, TimeOfDay newTime, System.Action onComplete)
    {
        fileId = FileIdGenerator.GetBGId(newLocation, newTime);

        changer.HideImmediate();
        changer.ShowWithCallback(fileId, () => onComplete?.Invoke());
    }

    public void ChangeBg(LocationType newLocation, TimeOfDay newTime, System.Action onComplete)
    {
        string newFileId = FileIdGenerator.GetBGId(newLocation, newTime);
        if (newFileId == fileId)
        {
            Debug.Log("変更なし");
            onComplete?.Invoke();
            return;
        }
        fileId = newFileId;
        changer.ChangeImage(fileId, () => onComplete?.Invoke(), 0.5f);
    }
}
