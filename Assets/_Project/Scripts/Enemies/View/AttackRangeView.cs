using UnityEngine;

namespace Project.Enemies.View
{
    public class AttackRangeView : MonoBehaviour
    {
        private const float ScaleMultiplier = 2f;

        private float _attackRange;
        private bool IsZeroAttackRange => _attackRange == 0f;

        public void Initialize(float attackRange)
        {
            _attackRange = attackRange;

            if (IsZeroAttackRange == false)
            {
                transform.localScale = attackRange * ScaleMultiplier * Vector3.one;
            }

            HideAttackRange();
        }

        public void ShowAttackRange()
        {
            if (IsZeroAttackRange)
                return;

            transform.gameObject.SetActive(true);
        }

        public void HideAttackRange()
        {
            transform.gameObject.SetActive(false);
        }
    }
}