using TMPro;
using UnityEngine;


public interface ITextHideAnimationStrategy
{
    /// <summary>
    /// �e�L�X�g���\���ɂ���A�j���[�V�������J�n���܂��B
    /// </summary>
    /// <param name="label">�A�j���[�V������K�p����TextMeshProUGUI�R���|�[�l���g�B</param>
    /// <param name="context">�R���[�`�����J�n���邽�߂�MonoBehaviour�B</param>
    /// <param name="onComplete">�A�j���[�V�����������ɌĂ΂��R�[���o�b�N�i�ȗ��\�j�B</param>
    void PlayHideAnimation(TextMeshProUGUI label, MonoBehaviour context, System.Action onComplete = null);

    /// <summary>
    /// �Đ����̃A�j���[�V�������X�L�b�v���A�����ɔ�\����Ԃɂ��܂��B
    /// </summary>
    void Skip();
}