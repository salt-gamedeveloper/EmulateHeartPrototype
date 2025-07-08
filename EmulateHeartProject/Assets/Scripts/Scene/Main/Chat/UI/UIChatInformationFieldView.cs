using UnityEngine;

public class UIChatInformationFieldView : UIFieldViewBase
{
    [SerializeField]
    private UITextView dayCountTextView;
    [SerializeField]
    private UIChatTimeOfDayField timeOfDayField;
    [SerializeField]
    private UIEmotionsField emotionsField;

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
        HideImmediate();
        SetDayCount(1);
        SetTimeOfDay(TimeOfDay.Morning);
        emotionsField.SceneReset();
    }

    public void SetDayCount(int dayCount)
    {
        dayCountTextView.SetText(dayCount.ToString("D2"));
        dayCountTextView.Show();
    }

    public void SetTimeOfDay(TimeOfDay time)
    {
        timeOfDayField.SetTimeOfDay(time);
        timeOfDayField.ChatCountReset();
    }

    public void AddChatCount()
    {
        timeOfDayField.AddChatCount();
    }
    public void AddEmotion(EmotionType emotion)
    {
        emotionsField.AddEmotion(emotion);
    }
}
