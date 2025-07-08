using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private AudioListener mainAudioListener;
    [SerializeField]
    private Canvas mainCanvas;
    [SerializeField]
    private Camera dialogCamera;
    [SerializeField]
    private AudioListener dialogAudioListener;
    [SerializeField]
    private Canvas dialogCanvas;
    [SerializeField]
    private UIRawImageView blurredBgRawImageView;
    [SerializeField]
    private RenderTexture sceneRenderTexture;
    [SerializeField]
    private GameObject globalVolume;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        SwitchToMain();
    }

    public void SwitchToDialog()
    {
        globalVolume.SetActive(true);
        mainCamera.enabled = true;
        mainAudioListener.enabled = true;
        mainCanvas.enabled = true;

        var captured = mainSceneCaptured();
        dialogCamera.enabled = false;
        dialogAudioListener.enabled = false;
        dialogCanvas.enabled = false;
        blurredBgRawImageView.Show(captured);
        dialogCamera.enabled = true;
        dialogAudioListener.enabled = true;
        dialogCanvas.enabled = true;
        mainCamera.enabled = false;
        mainAudioListener.enabled = false;
        mainCanvas.enabled = false;
    }

    public void SwitchToMain()
    {
        globalVolume.SetActive(false);
        mainCamera.enabled = true;
        mainAudioListener.enabled = true;
        mainCanvas.enabled = true;
        dialogCamera.enabled = false;
        dialogAudioListener.enabled = false;
        dialogCanvas.enabled = false;
    }

    private Texture2D mainSceneCaptured()
    {
        return CameraCaptureUtility.CaptureCameraImageWithCurrentScreenSize(mainCamera);
    }
}
