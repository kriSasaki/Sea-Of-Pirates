using UnityEngine;

namespace Project.Utils
{
    [RequireComponent(typeof(Camera))]
    public class LayerDistanceCuller : MonoBehaviour
    {
        const int EnvironmentLayer = 12;
        const float CullDistance = 150;

        void Start()
        {
            Camera camera = GetComponent<Camera>();
            float[] distances = new float[32];
            distances[EnvironmentLayer] = CullDistance;
            camera.layerCullDistances = distances;
        }
    }
}