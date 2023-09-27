using UnityEngine;

namespace Clones.GameLogic
{
    [ExecuteInEditMode]
    public class CameraShader : MonoBehaviour
    {
        [SerializeField] private Material _shaderTexture;

        public Material ShaderTexture => _shaderTexture;

        private void OnRenderImage(RenderTexture cameraView, RenderTexture shaderView)
        {
            Graphics.Blit(cameraView, shaderView, _shaderTexture);
        }
    }
}
