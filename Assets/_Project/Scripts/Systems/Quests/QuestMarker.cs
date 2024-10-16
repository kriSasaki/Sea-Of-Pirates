using UnityEngine;

namespace Scripts.Systems.Quests
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class QuestMarker : MonoBehaviour
    {
        [SerializeField] private Mesh _questionMesh;
        [SerializeField] private Mesh _exclamationMesh;
        [SerializeField] private Color _activeColor = Color.yellow;
        [SerializeField] private Color _takenColor = Color.gray;

        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetMarkerVisual(QuestState questState)
        {
            switch (questState)
            {
                case QuestState.Avaliable:
                    _meshFilter.mesh = _exclamationMesh;
                    _meshRenderer.material.color = _activeColor;
                    break;

                case QuestState.Taken:
                    _meshFilter.mesh = _questionMesh;
                    _meshRenderer.material.color = _takenColor;
                    break;

                case QuestState.Done:
                    _meshFilter.mesh = _questionMesh;
                    _meshRenderer.material.color = _activeColor;
                    break;

                case QuestState.Completed:
                    _meshRenderer.enabled = false;
                    break;
            }
        }
    }
}