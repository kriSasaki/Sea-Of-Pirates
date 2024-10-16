using DTT.AreaOfEffectRegions;
using UnityEngine;

namespace Scripts.Players.View
{
    public class ShipAttackCones : ArcRegionBase
    {
        private const float ScaleMultiplier = 2f;

        private static readonly int s_arcShaderID = Shader.PropertyToID("_Arc");
        private static readonly int s_progressShaderID = Shader.PropertyToID("_Progress");

        [SerializeField] private Transform _rightCone;
        [SerializeField] private Transform _leftCone;
        [SerializeField, Range(0, 1)] private float _fillProgress;
        [SerializeField] private MeshRenderer[] _maskedCentres;

        public float FillProgress
        {
            get => _fillProgress;
            set => _fillProgress = Mathf.Clamp01(value);
        }

        public void SetRadius(float radius)
        {
            SetConeRadius(_leftCone, radius);
            SetConeRadius(_rightCone, radius);
        }

        public void SetAngle(float angle)
        {
            Arc = angle;

            for (int i = 0; i < _maskedCentres.Length; i++)
                _maskedCentres[i].sharedMaterial.SetFloat(s_arcShaderID, 1 - Arc / 360);
        }

        public void SetProgress(float progress)
        {
            FillProgress = progress;

            for (int i = 0; i < _maskedCentres.Length; i++)
                _maskedCentres[i].sharedMaterial.SetFloat(s_progressShaderID, Mathf.Clamp01(_fillProgress));
        }

        private void SetConeRadius(Transform cone, float radius)
        {
            cone.localPosition = Vector3.zero;
            cone.localScale = radius * ScaleMultiplier * Vector3.one;
        }
    }
}