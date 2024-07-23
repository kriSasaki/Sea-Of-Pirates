using UnityEngine;

namespace Scripts.Players.PlayerLogic.Guns
{
    public class Gun : MonoBehaviour
    {
        private const string TurnOffFlash = "HideFlash";
        [SerializeField] private GameObject _particalObject;
        [SerializeField] private AudioSource _soundOfGunshot;
        [SerializeField] private float _minimalAudioPitch;
        [SerializeField] private float _maximalAudioPitch;
        [SerializeField] private float _effectTime;

        public void Attack()
        {
            _soundOfGunshot.pitch = Random.Range(_minimalAudioPitch, _maximalAudioPitch);
            _particalObject.SetActive(true);
            Invoke(TurnOffFlash, _effectTime);
        }

        private void HideFlash()
        {
            _particalObject.SetActive(false);
        }
    }
}