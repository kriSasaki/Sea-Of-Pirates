using Project.General.View;
using UnityEngine;

namespace Project.Players.View
{
    public class PlayerView : ShipView
    {
        [SerializeField] private ParticleSystem _waterTrail;
        [SerializeField] private Vector3 _waterLineOffset = new (0f, -1.3f, 0f);

        private void Awake()
        {
            SetOriginLocalPosition(transform.localPosition);
            InitializeShipSwinger(_waterLineOffset);
            _waterTrail.Play();
        }

        protected override void OnDie()
        {
            base.OnDie();
            _waterTrail.Stop();
        }

        protected override void OnRessurect()
        {
            base.OnRessurect();
            _waterTrail.Play();
        }
    }
}