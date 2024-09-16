using Project.Interfaces.SDK;

namespace Project.SDK
{
    public class GameReadyService : IGameReadyService
    {
        private bool _isCalled = false;

        public void Call()
        {
            if (_isCalled)
                return;

            Agava.YandexGames.YandexGamesSdk.GameReady();
            _isCalled = true;
        }
    }
}