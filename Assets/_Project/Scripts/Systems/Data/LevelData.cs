using UnityEngine;

namespace Scripts.Systems.Data
{
    [System.Serializable]
    public class LevelData
    {
        [SerializeField] private string _levelName;
        [SerializeField] private bool _isReachedPirateBay = false;

        public LevelData(string levelName)
        {
            _levelName = levelName;
        }

        public string LevelName => _levelName;
        public bool IsReachedPirateBay => _isReachedPirateBay;

        public void ReachPirateBay()
        {
            _isReachedPirateBay = true;
        }
    }
}