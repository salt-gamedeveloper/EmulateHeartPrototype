using UnityEngine;
using UnityEngine.UI;

public interface IImageHideAnimationStrategy
{
    void PlayHideAnimation(Image image, MonoBehaviour context, System.Action onComplete = null);

    /// <summary>
    /// �Đ����̃A�j���[�V�������X�L�b�v���A�����ɔ�\����Ԃɂ��܂��B
    /// </summary>
    void Skip();
}
