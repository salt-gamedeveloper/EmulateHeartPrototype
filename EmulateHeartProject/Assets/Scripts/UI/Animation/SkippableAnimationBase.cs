using System.Collections;
using UnityEngine;

/// <summary>
/// �X�L�b�v�\�ȃA�j���[�V�����̊��N���X�B�Ώ�UI�v�f�ւ̎Q�Ƃ͔h���N���X���ێ��B
/// </summary>
public abstract class SkippableAnimationBase
{
    protected Coroutine animationCoroutine;
    protected MonoBehaviour context;
    protected System.Action onComplete;

    /// <summary>
    /// �h���N���X����Ăяo���G���g���[�|�C���g�B
    /// </summary>
    public void Play(MonoBehaviour context, System.Action onComplete = null)
    {
        if (animationCoroutine != null && this.context != null)
        {
            this.context.StopCoroutine(animationCoroutine);
            Complete();
        }

        this.context = context;
        this.onComplete = onComplete;

        animationCoroutine = context.StartCoroutine(PlayWrapper());
    }

    private IEnumerator PlayWrapper()
    {
        yield return context.StartCoroutine(PlayRoutine());
        Complete();
    }

    public virtual void Skip()
    {
        if (animationCoroutine != null && context != null)
        {
            context.StopCoroutine(animationCoroutine);
            Complete();
        }
    }

    protected void Complete()
    {
        OnSkip();
        animationCoroutine = null;
        onComplete?.Invoke();
        onComplete = null;
    }

    /// <summary> �A�j���[�V�����{�̂̏��� </summary>
    protected abstract IEnumerator PlayRoutine();

    /// <summary> �X�L�b�v���̕�ԏ�Ԕ��f </summary>
    protected abstract void OnSkip();
}