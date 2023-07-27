using UnityEngine;

[ExecuteInEditMode]
public class CameraShader : MonoBehaviour
{
    [SerializeField] private Material _shaderTexture;

    private void OnRenderImage(RenderTexture cameraView, RenderTexture shaderView)
    {
        Graphics.Blit(cameraView, shaderView, _shaderTexture);
    }
}
