using UnityEngine;
using TMPro;

namespace Korakit
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        [SerializeField]
        public int score = 0;
        [SerializeField]
        public TMP_Text scoreText;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            UpdateScoreUI();
        }

        public void AddScore(int amount)
        {
            score += amount;
            UpdateScoreUI();
        }

        public void ReduceScore(int amount)
        {
            score = Mathf.Max(0, score - amount);
            UpdateScoreUI();
        }

        private void UpdateScoreUI()
        {
            if (scoreText == null)
                return;

            scoreText.text = $"Score: {score}";
        }
    }
}
