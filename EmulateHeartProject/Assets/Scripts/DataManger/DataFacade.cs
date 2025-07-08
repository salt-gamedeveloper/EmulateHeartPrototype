using UnityEngine;

public class DataFacade : MonoBehaviour
{
    private static DataFacade instance;

    private ContextDataManager context;
    private CharacterDataManager character;
    private ConversationDataManager conversation;


    public static DataFacade Instance => instance;
    public ContextDataManager Context => context;
    public CharacterDataManager Character => character;
    public ConversationDataManager Conversation => conversation;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        Initialize();
    }

    private void Initialize()
    {
        context = new ContextDataManager();
        character = new CharacterDataManager();
        conversation = new ConversationDataManager();
    }

    public void DataReset()
    {
        context = new ContextDataManager();
        character = new CharacterDataManager();
        conversation = new ConversationDataManager();
    }
}
