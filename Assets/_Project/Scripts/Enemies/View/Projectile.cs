using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Project.Enemies.View
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _splashParticles;
        [SerializeField] private MeshRenderer _projectileRenderer;

        private const float ScaleMiltiplier = 2f;
        private const float VisualRadiusOffset = 0.2f;

        public async UniTaskVoid InitializeAsync(float radius, float explodeDelay, LayerMask targetMask, Action onReachCallback)
        {
            Vector3 scale = ScaleMiltiplier * (radius + VisualRadiusOffset) * Vector3.one;

            await transform.DOScale(scale, explodeDelay);

            if (Physics.CheckSphere(transform.position, radius, targetMask))
            {
                onReachCallback();
            }
            else
            {
                _projectileRenderer.enabled = false;
                
                _splashParticles.Play();
                await UniTask.WaitUntil (() => _splashParticles.isPlaying == false);
            }

            Destroy(gameObject);
        }
    }
}