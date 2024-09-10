using DG.Tweening;
using Project.Utils.Extensions;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageLabel;
    [SerializeField] private Ease _ease;
    [SerializeField] private float _duration;
    [SerializeField] private float _maxScale = 2f;

    public void Initialize(int damage)
    {
        _damageLabel.text = damage.ToNumericalString();
        transform.localScale = Vector3.zero;

        transform.DOScale(_maxScale, _duration)
            .SetEase(_ease)
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(() => gameObject.SetActive(false));
    }
}