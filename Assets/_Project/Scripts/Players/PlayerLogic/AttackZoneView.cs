using UnityEngine;

public class AttackZoneView : MonoBehaviour
{
    [SerializeField] private ShipAttackCones _attackCones;

    public void SetRadius(float radius)
    {
        _attackCones.SetRadius(radius);
    }

    public void SetAngle(float angle)
    {
        _attackCones.SetAngle(angle);
    }

    public void SetProgress(float progress)
    {
        _attackCones.SetProgress(progress);
    }

    public void Show()
    {
        _attackCones.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _attackCones.gameObject.SetActive(false);
    }
}