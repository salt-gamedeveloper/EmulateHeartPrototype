using System.Collections;
using UnityEngine;

/// <summary>
/// スキップ可能なアニメーションの基底クラス。対象UI要素への参照は派生クラスが保持。
/// </summary>
public abstract class SkippableAnimationBase
{
    protected Coroutine animationCoroutine;
    protected MonoBehaviour context;
    protected System.Action onComplete;

    /// <summary>
    /// 派生クラスから呼び出すエントリーポイント。
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

    /// <summary> アニメーション本体の処理 </summary>
    protected abstract IEnumerator PlayRoutine();

    /// <summary> スキップ時の補間状態反映 </summary>
    protected abstract void OnSkip();
}