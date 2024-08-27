using Cysharp.Threading.Tasks;
using Project.Utils.Tweens;
using UnityEngine;

namespace Project.General.View
{
    public abstract class ShipView : MonoBehaviour
    {
       
        [SerializeField] private SinkTween _sinkTween;
        [SerializeField] private ShipSwinger _shipSwinger;

        private Vector3 _originLocalPosition;

        private void Start()
        {
            _sinkTween.Initialize(transform);
        }

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

        protected void InitializeShipSwinger(Vector3 waterLineOffset)
        {
            _shipSwinger.Initialize(waterLineOffset);
        }

        protected void SetOriginLocalPosition(Vector3 position)
        {
            _originLocalPosition = position;
        }
    }
}