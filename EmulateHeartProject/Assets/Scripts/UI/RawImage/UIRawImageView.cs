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
            Debug.LogError("RawImage���Z�b�g����Ă��܂���");
            return;
        }

        rawImage.texture = texture;
    }
}
