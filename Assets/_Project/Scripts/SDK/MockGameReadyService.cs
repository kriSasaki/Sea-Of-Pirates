using Project.Interfaces.SDK;
using UnityEngine;

namespace Project.SDK
{
    public class MockGameReadyService : IGameReadyService
    {
        public void Call()
        {
            Debug.Log("GameReady called");
        }
    }
}