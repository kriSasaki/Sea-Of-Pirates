using DG.Tweening;
using UnityEngine;

namespace Project.Utils
{
    public class SineMover : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _height;
        [SerializeField] private Ease _ease;

        private void Start()
        {
            transform.DOMoveY(_height, _duration)
                .SetRelative()
                .SetEase(_ease)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}