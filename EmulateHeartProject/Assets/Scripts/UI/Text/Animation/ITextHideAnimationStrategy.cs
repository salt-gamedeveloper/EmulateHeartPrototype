using TMPro;
using UnityEngine;


public interface ITextHideAnimationStrategy
{
    /// <summary>
    /// テキストを非表示にするアニメーションを開始します。
    /// </summary>
    /// <param name="label">アニメーションを適用するTextMeshProUGUIコンポーネント。</param>
    /// <param name="context">コルーチンを開始するためのMonoBehaviour。</param>
    /// <param name="onComplete">アニメーション完了時に呼ばれるコールバック（省略可能）。</param>
    void PlayHideAnimation(TextMeshProUGUI label, MonoBehaviour context, System.Action onComplete = null);

    /// <summary>
    /// 再生中のアニメーションをスキップし、即座に非表示状態にします。
    /// </summary>
    void Skip();
}