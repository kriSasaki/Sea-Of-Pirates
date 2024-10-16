using Cysharp.Threading.Tasks;
using Scripts.Utils.Extensions;
using Scripts.Utils.Tweens;
using UnityEngine;

namespace Scripts.General.View
{
    public abstract class ShipView : MonoBehaviour
    {
        private const float GizmoLineLenght = 20f;

        [SerializeField] private float _verticalOffset = 0.5f;
        [SerializeField] private ShipSwinger _shipSwinger;
        [SerializeField] private SinkTween _sinkTween;

        private Vector3 _originLocalPosition;

        private void Awake()
        {
            transform.localPosition = transform.localPosition.AddY(-_verticalOffset);
            _originLocalPosition = transform.localPosition;

            _sinkTween.Initialize(transform);
            _shipSwinger.Initialize();
        }

        public abstract void TakeDamage(int damage);

        public async UniTask DieAsync()
        {
            _shipSwinger.enabled = false;
            OnDie();

            await _sinkTween.Sink(destroyCancellationToken);
        }

        public void Ressurect()
        {
            transform.SetLocalPositionAndRotation(_originLocalPosition, Quaternion.identity);
            _shipSwinger.enabled = true;
            OnRessurect();
        }

        protected virtual void OnDie()
        {
        }

        protected virtual void OnRessurect()
        {
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(
                transform.position.AddY(_verticalOffset),
                transform.position.AddY(_verticalOffset) + transform.forward * GizmoLineLenght);
        }
    }
}