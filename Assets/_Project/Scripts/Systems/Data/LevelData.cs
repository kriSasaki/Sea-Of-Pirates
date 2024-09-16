using UnityEngine;

namespace Project.Systems.Data
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

        public void ReachPirateBay()
        {
            _isReachedPirateBay = true;
        }

        public bool IsReachedPirateBay => _isReachedPirateBay;
        public string LevelName => _levelName;
    }
}