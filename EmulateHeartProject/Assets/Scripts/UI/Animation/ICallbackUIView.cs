public interface ICallbackUIView : IUIView
{
    /// <summary>
    /// �R�[���o�b�N�t����UI��\�����܂��B
    /// </summary>
    /// <param name="onComplete">�\���A�j���[�V�����������ɌĂяo���R�[���o�b�N</param>
    void ShowWithCallback(System.Action onComplete);

    /// <summary>
    /// �R�[���o�b�N�t����UI���\���ɂ��܂��B
    /// </summary>
    /// <param name="onComplete">��\���A�j���[�V�����������ɌĂяo���R�[���o�b�N</param>
    void HideWithCallback(System.Action onComplete);

    /// <summary>
    /// �\���A�j���[�V�������X�L�b�v���AUI�𑦎��ɕ\����Ԃɂ��܂��B
    /// </summary>
    void ShowImmediate();

    /// <summary>
    /// ��\���A�j���[�V�������X�L�b�v���AUI�𑦎��ɔ�\����Ԃɂ��܂��B
    /// </summary>
    void HideImmediate();
}
