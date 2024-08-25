using DG.Tweening;
using Project.Utils.Tweens;
using Project.Utils.VFX;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerAttackView : MonoBehaviour
{
    private const float MinProgress = 0f;
    private const float MaxProgress = 1f;

    [SerializeField] private ShipAttackCones _attackCones;
    [SerializeField] private AppearingTransformTween _appearingTween;
    [SerializeField] private Collider _shipCollider;
    [SerializeField] private Ease _reloadEase = Ease.InOutSine;
    
    private VfxSpawner _vfxSpawner;

    [Inject]
    public void Construct (VfxSpawner vfxSpawner)
    {
        _vfxSpawner = vfxSpawner;
        _appearingTween.Initialize(_attackCones.transform);
        _attackCones.gameObject.SetActive(false);
    }

    public void SetRange(float attackRange)
    {
        _attackCones.SetRadius(attackRange);
    }

    public void SetAngle(float angle)
    {
        _attackCones.SetAngle(angle);
    }

    public void SetProgress(float progress)
    {
        _attackCones.SetProgress(progress);
    }

    public void Shoot(Vector3 targetPosition)
    {
        _vfxSpawner.SpawnCannonSmoke(_shipCollider, targetPosition);
    }


    public IEnumerator Reloading(float cooldown)
    {
        yield return DOTween.To(SetProgress, MinProgress, MaxProgress, cooldown).SetEase(_reloadEase).WaitForKill();
    }

    public void Show()
    {
        _attackCones.gameObject.SetActive(true);
        _appearingTween.Appear();
    }

    public void Hide()
    {
        _appearingTween.Disappear(() => _attackCones.gameObject.SetActive(false));
    }
}