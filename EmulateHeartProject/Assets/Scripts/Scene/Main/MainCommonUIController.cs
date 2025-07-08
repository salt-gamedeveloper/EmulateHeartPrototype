using UnityEngine;

public class MainCommonUIController : UIControllerBase
{
    [SerializeField]
    private UIButtonView sideMenuButtonView;

    private System.Action OnSideMenuClicked;
    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        Hide();
    }
    public void Open()
    {
        Show();
    }
    public void Close()
    {
        Hide();
    }

    public void SubscribeToSideMenu(System.Action listener)
    {
        OnSideMenuClicked += listener;
        sideMenuButtonView.SubscribeToButtonClick(OnSideMenuClicked);
    }
}
