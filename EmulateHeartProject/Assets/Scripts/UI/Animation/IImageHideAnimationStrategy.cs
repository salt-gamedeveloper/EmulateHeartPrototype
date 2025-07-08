using UnityEngine;
using UnityEngine.UI;

public interface IImageHideAnimationStrategy
{
    void PlayHideAnimation(Image image, MonoBehaviour context, System.Action onComplete = null);

    /// <summary>
    /// 再生中のアニメーションをスキップし、即座に非表示状態にします。
    /// </summary>
    void Skip();
}
