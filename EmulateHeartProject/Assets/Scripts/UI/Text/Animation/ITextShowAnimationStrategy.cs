using TMPro;
using UnityEngine;

public interface ITextShowAnimationStrategy
{
    /// <summary>
    /// テキストを表示するアニメーションを開始します。
    /// </summary>
    /// <param name="label">アニメーションを適用するTextMeshProUGUIコンポーネント。</param>
    /// <param name="text">表示するテキスト文字列。</param>
    /// <param name="context">コルーチンを開始するためのMonoBehaviour。</param>
    /// <param name="onComplete">アニメーション完了時に呼ばれるコールバック（省略可能）。</param>
    void PlayShowAnimation(TextMeshProUGUI label, string text, MonoBehaviour context, System.Action onComplete = null);

    /// <summary>
    /// 再生中の表示アニメーションをスキップし、即座にテキストを完全表示します。
    /// </summary>
    void Skip();
}
