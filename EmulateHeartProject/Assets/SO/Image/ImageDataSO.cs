using UnityEngine;

[CreateAssetMenu(fileName = "ImageData", menuName = "Image/ImageData")]
public class ImageDataSO : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private Sprite sprite;

    public string Id => id;
    public Sprite Sprite => sprite;

}
