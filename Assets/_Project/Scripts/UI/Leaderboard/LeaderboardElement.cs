using TMPro;
using UnityEngine;

namespace Project.UI.Leaderboard
{
    public class LeaderboardElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _playerRank;
        [SerializeField] private TMP_Text _playerScore;

        public void Initialize(string name, int rank, int score)
        {
            _playerName.text = name;
            _playerRank.text = rank.ToString();
            _playerScore.text = score.ToString();
        }

        public void Initialize(string name, int rank, int score, Color textColor)
        {
            _playerName.color = textColor;
            _playerRank.color = textColor;
            _playerScore.color = textColor;

            Initialize(name, rank, score);
        }
    }
}