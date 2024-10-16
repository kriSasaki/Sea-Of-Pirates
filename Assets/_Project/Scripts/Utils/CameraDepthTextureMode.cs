using UnityEngine;

namespace Scripts.Utils
{
    public class CameraDepthTextureMode : MonoBehaviour
    {
        [SerializeField] private DepthTextureMode _depthTextureMode;

        private void OnValidate()
        {
            SetCameraDepthTextureMode();
        }

        private void Awake()
        {
            SetCameraDepthTextureMode();
        }

        private void SetCameraDepthTextureMode()
        {
            GetComponent<Camera>().depthTextureMode = _depthTextureMode;
        }
    }
}