using System.Collections.Generic;
using UnityEngine;

public class UIChatOptionsField : MonoBehaviour
{
    [SerializeField]
    private GameObject normalOption;
    [SerializeField]
    private GameObject joyOption;
    [SerializeField]
    private GameObject angerOption;
    [SerializeField]
    private GameObject sorrowOption;
    [SerializeField]
    private GameObject funOption;

    private Dictionary<EmotionType, GameObject> emotionOptions;

    private System.Action<string> OnOptionSelected;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        SetpuDictionary();
    }

    public void SceneReset()
    {
        foreach (Transform n in transform)
        {
            Destroy(n.gameObject);
        }
    }

    public void SetOptions(List<PlayerOption> playerOptions)
    {
        foreach (Transform n in transform)
        {
            Destroy(n.gameObject);
        }

        foreach (PlayerOption option in playerOptions)
        {
            if (option != null)
            {
                CreateOption(option);
            }
        }
    }

    private void CreateOption(PlayerOption option)
    {
        EmotionType emotion = option.OptionEmotion;
        string message = option.OptionMessage;
        GameObject newOption = Instantiate(emotionOptions[emotion], transform);

        UITextView optionTextView = newOption.GetComponentInChildren<UITextView>();
        UIButtonView optionButtonView = newOption.GetComponentInChildren<UIButtonView>();
        if (optionTextView != null && optionButtonView != null)
        {
            optionTextView.SetText(message);
            optionTextView.Show();
            optionButtonView.SubscribeToButtonClick(() => OnOptionSelected(message));
        }
    }

    public void StartResponseWait()
    {
        foreach (Transform n in transform)
        {
            UIButtonView optionButtonView = n.GetComponentInChildren<UIButtonView>();
            if (optionButtonView != null)
            {
                optionButtonView.SetButtonEnabled(false);
            }
        }
    }

    public void StartInputWait()
    {
        foreach (Transform n in transform)
        {
            UIButtonView optionButtonView = n.GetComponentInChildren<UIButtonView>();
            if (optionButtonView != null)
            {

                optionButtonView.SetButtonEnabled(true);
            }
        }
    }

    private void SetpuDictionary()
    {
        emotionOptions = new Dictionary<EmotionType, GameObject>()
        {
            {EmotionType.Normal,normalOption},
            {EmotionType.Joy, joyOption},
            {EmotionType.Anger,angerOption},
            {EmotionType.Sorrow, sorrowOption},
            {EmotionType.Fun, funOption},
        };
    }

    public void SubscribeToOptionSelected(System.Action<string> listener)
    {
        OnOptionSelected = listener;
    }
}
