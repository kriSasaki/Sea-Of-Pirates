using Scripts.Interfaces.SDK;
using YG;

namespace Scripts.SDK
{
    public class GameReadyService : IGameReadyService
    {
        private bool _isCalled = false;

        public void Call()
        {
            if (_isCalled)
                return;

            YandexGame.GameReadyAPI();
            _isCalled = true;
        }
    }
}