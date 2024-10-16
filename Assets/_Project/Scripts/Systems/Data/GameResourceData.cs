using UnityEngine;

namespace Scripts.Systems.Data
{
    [System.Serializable]
    public class GameResourceData
    {
        [SerializeField] private string _id;
        [SerializeField] private int _value;

        public string ID => _id;
        public int Value => _value;

        public GameResourceData(string id, int value)
        {
            _id = id;
            _value = value;
        }

        public void ChangeResourceAmount(int value)
        {
            _value = value;
        }
    }
}