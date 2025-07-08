using UnityEngine;
using UnityEngine.UI;

public interface IImageShowAnimationStrategy
{
    void PlayShowAnimation(Image image, MonoBehaviour context, System.Action onComplete = null);
    void Skip();
}