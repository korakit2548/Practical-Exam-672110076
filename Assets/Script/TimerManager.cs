using UnityEngine;
using TMPro;

namespace Korakit
{
    public class TimerManager : MonoBehaviour
    {
        public static TimerManager Instance { get; private set; }

        [SerializeField] public float startTime = 30f;
        [SerializeField] public TMP_Text timerText;

        public float RemainingTime { get; private set; }
        public bool IsTimeUp { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            RemainingTime = startTime;
            UpdateTimerUI();
        }
        private void Update()
        {
            if (IsTimeUp)
                return;

            RemainingTime -= Time.deltaTime;

            if (RemainingTime <= 0f)
            {
                RemainingTime = 0f;
                EndGameByTime();
            }

            UpdateTimerUI();
        }
        public void AddTime(float amount)
        {
            if (IsTimeUp)
                return;

            RemainingTime += amount;
            UpdateTimerUI();
        }
        public void ReduceTime(float amount)
        {
            if (IsTimeUp)
                return;

            RemainingTime -= amount;

            if (RemainingTime <= 0f)
            {
                RemainingTime = 0f;
                IsTimeUp = true;
                UpdateTimerUI();

                Debug.Log("เวลาเหลือ 0 จากการเก็บ Item");

                SceneManagement manager =
                    FindFirstObjectByType<SceneManagement>();

                if (manager != null)
                {
                    int finalScore = ScoreManager.Instance != null
                        ? ScoreManager.Instance.score
                        : 0;

                    if (finalScore > 9999)
                        manager.WinGame();
                    else
                        manager.LoseGame();
                }
                else
                {
                    Debug.LogError("ไม่พบ SceneManagement ใน Scene นี้");
                }

                return;
            }

            UpdateTimerUI();
        }
        private void EndGameByTime()
        {
            if (IsTimeUp)
                return;

            IsTimeUp = true;
            UpdateTimerUI();

            SceneManagement manager = FindFirstObjectByType<SceneManagement>();

            int finalScore = ScoreManager.Instance != null ? ScoreManager.Instance.score : 0;

            if (manager != null)
            {
                if (finalScore > 9999)
                {
                    manager.WinGame();
                }
                else
                {
                    manager.LoseGame();
                }
            }
        }
        private void UpdateTimerUI()
        {
            if (timerText == null)
                return;

            int seconds = Mathf.CeilToInt(RemainingTime);
            timerText.text =
                $"Time: {seconds / 60:00}:{seconds % 60:00}";
        }
    }
}
