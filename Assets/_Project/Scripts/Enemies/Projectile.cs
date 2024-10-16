using System;
using Ami.BroAudio;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Scripts.Interfaces.Audio;
using UnityEngine;

namespace Scripts.Enemies
{
    public class Projectile : MonoBehaviour
    {
        private const float ScaleMiltiplier = 2f;
        private const float VisualRadiusOffset = 0.2f;

        [SerializeField] private ParticleSystem _splashParticles;
        [SerializeField] private MeshRenderer _projectileRenderer;
        [SerializeField] private SoundID _splashSound;

        private IAudioService _audioService;

        public void Initialize(IAudioService audioService)
        {
            _audioService ??= audioService;
        }

        public async UniTaskVoid ShootAsync(ProjectileSettings settings, Action onReachCallback)
        {
            Vector3 scale = ScaleMiltiplier * (settings.Radius + VisualRadiusOffset) * Vector3.one;
            transform.localScale = Vector3.one;
            _projectileRenderer.enabled = true;

            await transform.DOScale(scale, settings.ExplodeDelay);

            if (Physics.CheckSphere(transform.position, settings.Radius, settings.TargetMask))
            {
                onReachCallback();
            }
            else
            {
                _projectileRenderer.enabled = false;

                _splashParticles.Play();
                _audioService.PlaySound(_splashSound);

                await UniTask.WaitUntil(() => _splashParticles.isPlaying == false);
            }

            gameObject.SetActive(false);
        }
    }
}