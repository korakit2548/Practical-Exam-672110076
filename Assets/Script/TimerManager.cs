using UnityEngine;
using TMPro;

namespace Korakit
{
    public class TimerManager : MonoBehaviour
    {
        public static TimerManager Instance { get; private set; }

        [SerializeField]
        public float startTime = 30f;
        [SerializeField]
        public TMP_Text timerText;

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
                IsTimeUp = true;
                Debug.Log("Time's Up!");
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

            RemainingTime = Mathf.Max(0f, RemainingTime - amount);

            if (RemainingTime <= 0f)
            {
                IsTimeUp = true;
                Debug.Log("Time's Up!");
            }

            UpdateTimerUI();
        }
        private void UpdateTimerUI()
        {
            if (timerText == null)
                return;

            int seconds = Mathf.CeilToInt(RemainingTime);
            timerText.text = $"Time: {seconds / 60:00}:{seconds % 60:00}";
        }
    }
}
