using System.Collections;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    private CanvasSwitcher canvasSwitcher;
    [SerializeField]
    private DialogCanvasController dialogCanvasController;
    [SerializeField]
    private SceneManager sceneManager;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        dialogCanvasController.CloseAllDialogs(null);
    }

    public void OpenChatToLog(System.Action onClose = null)
    {
        canvasSwitcher.SwitchToDialog();
        dialogCanvasController.OpenChatToLog();

        dialogCanvasController.SubscribeToChatLogClose(() =>
        {
            onClose?.Invoke();
            CloseDialog();
        });
    }
    public void OpenSideMenu(System.Action onClose = null)
    {
        canvasSwitcher.SwitchToDialog();
        dialogCanvasController.OpenSideMenu();

        dialogCanvasController.SubscribeToSideMenuClose(() =>
        {
            onClose?.Invoke();
            CloseDialog();
        });
        dialogCanvasController.SubscribeToSideMenuSceneChange(() =>
        {
            //TODO: CloseDialogのスキップ
            //FIXME:サイドメニューからシーン移行時、シーンのShowが見えない
            onClose?.Invoke();
            CloseDialog();
        });
    }

    public void OpenSmallDialog(SmallDialogViewModel viewModel, System.Action onClose = null)
    {
        canvasSwitcher.SwitchToDialog();
        dialogCanvasController.OpenSmallDialog(viewModel);

        dialogCanvasController.SubscribeToDialogClose(() =>
        {
            onClose?.Invoke();
            CloseDialog();
        });
        dialogCanvasController.SubscribeToDialogYes(() =>
        {
            onClose?.Invoke();
            CloseDialog();
        });
        dialogCanvasController.SubscribeToDialogNo(() =>
        {
            onClose?.Invoke();
            CloseDialog();
        });
    }

    /// <summary>
    /// すべてのダイアログを閉じて、メイン画面に戻す
    /// </summary>
    public void CloseDialog()
    {
        dialogCanvasController.CloseAllDialogs(() =>
        {
            canvasSwitcher.SwitchToMain();
            StartCoroutine(DelayAndReopen());
        });
    }
    private IEnumerator DelayAndReopen()
    {
        yield return new WaitForSeconds(0.5f); // ← 任意のディレイ時間
        sceneManager.ReopenScene();
    }
}
