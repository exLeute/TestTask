using UnityEngine;

namespace VFX
{
    public class CameraPostprocessing : MonoBehaviour
    {
        [SerializeField] private Material postprocessMaterial;

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (postprocessMaterial == null)
            {
                Graphics.Blit(source, destination);
                return;
            }
        
            Graphics.Blit(source, destination, postprocessMaterial);
        }
    }
}
