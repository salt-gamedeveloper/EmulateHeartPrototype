using UnityEngine;
using UnityEngine.UI;

public class ImageAnimatorAnimation : MonoBehaviour, IImageShowAnimationStrategy
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string showTriggerName = "Play";

    private System.Action onCompleteCallback;
    public void PlayShowAnimation(Image image, MonoBehaviour context, System.Action onComplete = null)
    {
        Color color = image.color;
        color.a = 1f;
        image.color = color;
        if (animator == null)
        {
            Debug.LogWarning("Animator is not assigned.");
            onComplete?.Invoke();
            return;
        }

        onCompleteCallback = onComplete; // ← コールバックを記録

        animator.SetTrigger(showTriggerName);
    }
    public void Skip()
    {

    }
    public void AnimationComplete()
    {
        onCompleteCallback?.Invoke();
        onCompleteCallback = null;
    }
}
