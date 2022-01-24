using UnityEngine;
using UnityEngine.Rendering.Universal;

[ExecuteInEditMode]
public class BlitTest : MonoBehaviour
{
    public Material mat;
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }
}
