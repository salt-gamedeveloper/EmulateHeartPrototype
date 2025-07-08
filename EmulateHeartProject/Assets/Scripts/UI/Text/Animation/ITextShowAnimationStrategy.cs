using TMPro;
using UnityEngine;

public interface ITextShowAnimationStrategy
{
    /// <summary>
    /// �e�L�X�g��\������A�j���[�V�������J�n���܂��B
    /// </summary>
    /// <param name="label">�A�j���[�V������K�p����TextMeshProUGUI�R���|�[�l���g�B</param>
    /// <param name="text">�\������e�L�X�g������B</param>
    /// <param name="context">�R���[�`�����J�n���邽�߂�MonoBehaviour�B</param>
    /// <param name="onComplete">�A�j���[�V�����������ɌĂ΂��R�[���o�b�N�i�ȗ��\�j�B</param>
    void PlayShowAnimation(TextMeshProUGUI label, string text, MonoBehaviour context, System.Action onComplete = null);

    /// <summary>
    /// �Đ����̕\���A�j���[�V�������X�L�b�v���A�����Ƀe�L�X�g�����S�\�����܂��B
    /// </summary>
    void Skip();
}
