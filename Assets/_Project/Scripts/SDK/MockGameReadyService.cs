using Scripts.Interfaces.SDK;
using UnityEngine;

namespace Scripts.SDK
{
    public class MockGameReadyService : IGameReadyService
    {
        public void Call()
        {
            Debug.Log("GameReady called");
        }
    }
}