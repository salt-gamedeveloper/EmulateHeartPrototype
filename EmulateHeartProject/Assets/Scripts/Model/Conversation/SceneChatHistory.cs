using System.Collections.Generic;
using System.Linq;

public class SceneChatHistory
{
    private List<MessageData> sceneMessages;
    public List<MessageData> SceneMessages => sceneMessages;

    public SceneChatHistory()
    {
        sceneMessages = new List<MessageData>();
    }

    public void AddMessage(MessageData newMessage)
    {
        sceneMessages.Add(newMessage);
    }

    public string ToChatPromptString()
    {
        // �ŐV��10���̃��b�Z�[�W�A�܂��͂��ꖢ���̃��b�Z�[�W���擾���܂�
        List<MessageData> recentMessages = sceneMessages
                                            .OrderByDescending(m => sceneMessages.IndexOf(m)) // �ŐV�̃��b�Z�[�W���擪�ɂȂ�悤�ɕ��בւ��܂�
                                            .Take(10) // �ő�10���擾���܂�
                                            .Reverse() // �v�����v�g�ł͌Â����̂���V�������̂֏��ɕ\�������悤�ɕ��בւ��܂�
                                            .ToList();

        // �e���b�Z�[�W���uMessageType: Message�v�̌`���Ō������܂�
        return string.Join("\n", recentMessages.Select(m => $"{m.MessageType}: {m.Message}"));
    }
}
