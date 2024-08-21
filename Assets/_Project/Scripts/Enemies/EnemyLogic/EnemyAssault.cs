using System;
using DG.Tweening;
using Project.Players.PlayerLogic;
using UnityEngine;

namespace Project.Enemies.EnemyLogic
{
    public class EnemyAssault : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;

        private Player _target;

        private void Update()
        {
            Chase();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _target = player;
                enabled = true;
            }
        }

        private void Chase()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        }
    }
}