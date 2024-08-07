using UnityEngine;
using DG.Tweening;

namespace Project.Utils
{
    public class SineMover : MonoBehaviour
    {
        [SerializeField] float _duration;
        [SerializeField] float _height;
        [SerializeField] Ease _ease;

        private void Start()
        {
            transform.DOMoveY(_height, _duration)
                .SetRelative()
                .SetEase(_ease)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}