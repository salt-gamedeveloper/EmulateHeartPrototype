using UnityEngine;

public static class CameraCaptureUtility
{
    public static Texture2D CaptureCameraImageWithCurrentScreenSize(Camera camera)
    {
        int width = Screen.width;
        int height = Screen.height;

        RenderTexture rt = RenderTexture.GetTemporary(width, height, 24);
        float originalAspect = camera.aspect;
        camera.aspect = (float)width / height;

        RenderTexture currentRT = RenderTexture.active;
        camera.targetTexture = rt;
        camera.Render();

        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        RenderTexture.active = currentRT;
        camera.targetTexture = null;
        camera.aspect = originalAspect;

        RenderTexture.ReleaseTemporary(rt);

        return tex;
    }
}
