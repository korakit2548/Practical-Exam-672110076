using UnityEngine;

namespace Korakit
{
    public class CollectibleItem : MonoBehaviour
    {
        public ItemType itemType;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            if (TimerManager.Instance == null || TimerManager.Instance.IsTimeUp)
                return;

            switch (itemType)
            {
                case ItemType.AddTime:
                    TimerManager.Instance.AddTime(3f);
                    break;

                case ItemType.ReduceTime:
                    TimerManager.Instance.ReduceTime(5f);
                    break;

                case ItemType.AddScore:
                    ScoreManager.Instance.AddScore(500);
                    break;

                case ItemType.ReduceScore:
                    ScoreManager.Instance.ReduceScore(500);
                    break;
            }

            Destroy(gameObject);

        }
    }
}
