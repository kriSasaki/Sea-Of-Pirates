using UnityEngine;

namespace Scripts.Utils
{
    [RequireComponent(typeof(Camera))]
    public class LayerDistanceCuller : MonoBehaviour
    {
        private const int EnvironmentLayer = 12;
        private const float CullDistance = 150;
        private const int LayerAmount = 32;

        private void Start()
        {
            Camera camera = GetComponent<Camera>();
            float[] distances = new float[LayerAmount];
            distances[EnvironmentLayer] = CullDistance;
            camera.layerCullDistances = distances;
        }
    }
}