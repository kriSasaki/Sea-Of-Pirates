using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scripts.Interfaces.Enemies
{
    public interface IPoolableEnemy : IEnemy
    {
       Transform Transform { get; }

       void Respawn(Vector3 atPosition);
       UniTask SinkAsync();
    }
}