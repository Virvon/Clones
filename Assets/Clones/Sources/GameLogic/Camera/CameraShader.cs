using UnityEngine;

namespace Clones.GameLogic
{
    [ExecuteInEditMode]
    public class CameraShader : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Material _shaderTexture;

        public UnityEngine.Material ShaderTexture => _shaderTexture;

        private void OnRenderImage(RenderTexture cameraView, RenderTexture shaderView) => 
            Graphics.Blit(cameraView, shaderView, _shaderTexture);
    }
}
