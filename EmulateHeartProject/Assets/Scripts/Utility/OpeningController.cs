using System.Collections;
using UnityEngine;

public class OpeningController : UIControllerBase
{
    [SerializeField]
    private UIImageView logoImageView;

    private System.Action GameStarat;
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        logoImageView.ShowWithCallback(() => StartCoroutine(LogoDelay()));
    }

    private IEnumerator LogoDelay()
    {
        yield return new WaitForSeconds(2f);
        Hide();
        logoImageView.HideWithCallback(() =>
            {
                Hide();
                GameStarat?.Invoke();
            });
    }

    public void SubscribeToGameStart(System.Action listener)
    {
        GameStarat = listener;
    }
}
