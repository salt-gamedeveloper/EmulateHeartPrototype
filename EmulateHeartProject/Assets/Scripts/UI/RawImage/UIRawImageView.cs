using UnityEngine;
using UnityEngine.UI;

public class UIRawImageView : MonoBehaviour
{
    [SerializeField]
    private RawImage rawImage;

    public void Show(Texture texture)
    {
        if (rawImage == null)
        {
            Debug.LogError("RawImage‚ªƒZƒbƒg‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");
            return;
        }

        rawImage.texture = texture;
    }
}
