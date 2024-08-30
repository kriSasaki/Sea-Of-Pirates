using Project.General.View;
using UnityEngine;

namespace Project.Players.View
{
    public class PlayerView : ShipView
    {
        [SerializeField] private ParticleSystem _waterTrail;

        private void Start()
        {
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