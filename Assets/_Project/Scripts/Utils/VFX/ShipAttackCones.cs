using DTT.AreaOfEffectRegions;
using UnityEngine;

public class ShipAttackCones : ArcRegionBase
{
    private static readonly int ArcShaderID = Shader.PropertyToID("_Arc");

    private static readonly int ProgressShaderID = Shader.PropertyToID("_Progress");

    public float FillProgress
    {
        get => _fillProgress;
        set => _fillProgress = Mathf.Clamp01(value);
    }

    [SerializeField]
    private Transform _rightCone;
    [SerializeField]
    private Transform _leftCone;

    [SerializeField]
    [Range(0, 1)]
    private float _fillProgress;

    [SerializeField]
    private MeshRenderer[] _maskedCentres;

    public void SetRadius(float radius)
    {
        _rightCone.localPosition = Vector3.zero;
        _rightCone.localScale = radius * 2 * Vector3.one;

        _leftCone.localPosition = Vector3.zero;
        _leftCone.localScale = radius * 2 * Vector3.one;
    }

    public void SetAngle(float angle)
    {
        Arc = angle;

        for (int i = 0; i < _maskedCentres.Length; i++)
            _maskedCentres[i].sharedMaterial.SetFloat(ArcShaderID, 1 - Arc / 360);
    }

    public void SetProgress(float progress)
    {
        FillProgress = progress;

        for (int i = 0; i < _maskedCentres.Length; i++)
            _maskedCentres[i].sharedMaterial.SetFloat(ProgressShaderID, Mathf.Clamp01(_fillProgress));
    }
}