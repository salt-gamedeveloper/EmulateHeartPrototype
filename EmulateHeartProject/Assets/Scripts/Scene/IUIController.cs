/// <summary>
/// UI�̐�����s�����߂̃C���^�[�t�F�[�X�ł��B
/// �eUI�v�f�͂��̃C���^�[�t�F�[�X���������邱�ƂŁA���ʂ̃��C�t�T�C�N���Ǘ���\������𓝈�I�Ɉ������Ƃ��ł��܂��B
/// </summary>
public interface IUIController
{
    /// <summary>
    /// UI�̏������������s���܂��B
    /// �ʏ�͓�����Ԃ̏�������C�x���g�o�^�Ȃǂ��s���܂��B
    /// </summary>
    void Initialize();

    /// <summary>
    /// ViewModel�𒍓����܂��B
    /// MVVM�p�^�[���ɏ]���AUI�ƃf�[�^�̐ڑ��ɗp�����܂��B
    /// </summary>
    /// <param name="viewModel">UI�Ƀo�C���h����ViewModel�B</param>
    void InjectViewModel(object viewModel);

    /// <summary>
    /// UI���J���܂��i�\���Ə������̃g���K�[�Ȃǁj�B
    /// �A�j���[�V������f�[�^�ǂݍ��݂Ȃǂ��܂܂��ꍇ������܂��B
    /// </summary>
    void Open();

    /// <summary>
    /// UI����܂��B
    /// �A�j���[�V������㏈���𔺂����Ƃ�����܂��B
    /// </summary>
    void Close();

    /// <summary>
    /// UI��\�����܂��B
    /// �ꎞ�I�ȕ\��/��\���̐؂�ւ��Ɏg�p���܂��iOpen�Ƃ͈قȂ�Ӗ��Łj�B
    /// </summary>
    void Show();

    /// <summary>
    /// UI���\���ɂ��܂��B
    /// �ꎞ�I�ɉ�ʂ���B���p�r�Ŏg�p����܂��B
    /// </summary>
    void Hide();
}