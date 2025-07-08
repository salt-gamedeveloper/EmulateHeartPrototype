using UnityEngine;

//FIXME:Chat回数と表示が一致しない場合がある
public class UIChatTimeOfDayField : MonoBehaviour
{
    [SerializeField]
    private UIImageChanger timeOfDayPanelChanger;
    [SerializeField]
    private Transform chatCountTransform;
    [SerializeField]
    private GameObject chatCountItem;

    private int itemCount;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        itemCount = 0;
        SetTimeOfDay(TimeOfDay.Morning);
    }

    public void AddChatCount()
    {
        //Debug.Log("カウント進める");
        if (itemCount < 10)
        {
            Instantiate(chatCountItem, chatCountTransform);
            itemCount++;
        }
    }

    public void ChatCountReset()
    {
        foreach (Transform n in chatCountTransform)
        {
            Destroy(n.gameObject);
        }
        itemCount = 0;
    }

    public void SetTimeOfDay(TimeOfDay timeOfDay)
    {
        if (timeOfDay == TimeOfDay.None) return;
        string id = $"{Enums.TimeOfDayToLowerString(timeOfDay)}_pnl";
        timeOfDayPanelChanger.ChangeImage(id);
    }
}
