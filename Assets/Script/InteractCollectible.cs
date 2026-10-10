using UnityEngine;

namespace Korakit
{
    public class InteractCollectible : MonoBehaviour
    {
        public ItemType itemType;

        [SerializeField] private GameObject interactPrompt;

        private PlayerController nearbyPlayer;
        private Camera mainCamera;

        private void Awake() 
        { 
            mainCamera = Camera.main;
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }
        }
        private void LateUpdate()
        {
            if (interactPrompt == null || mainCamera == null) 
                return;

            interactPrompt.transform.rotation = Quaternion.LookRotation(interactPrompt.transform.position - mainCamera.transform.position);
        }    
        private void OnTriggerEnter(Collider other)
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();

            if (player == null)
                return;

            nearbyPlayer = player;
            nearbyPlayer.SetNearbyItem(this);

            if (interactPrompt != null)
            {
                interactPrompt.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            PlayerController player =
                other.GetComponentInParent<PlayerController>();

            if (player == null || player != nearbyPlayer)
                return;

            player.SetNearbyItem(null);
            nearbyPlayer = null;

            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }
        }

        public void Interact()
        {
            if (nearbyPlayer == null)
                return;

            if (TimerManager.Instance == null ||
                TimerManager.Instance.IsTimeUp)
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
                    if (ScoreManager.Instance != null)
                        ScoreManager.Instance.AddScore(500);
                    break;

                case ItemType.ReduceScore:
                    if (ScoreManager.Instance != null)
                        ScoreManager.Instance.ReduceScore(500);
                    break;
            }

            nearbyPlayer.SetNearbyItem(null);
            Destroy(gameObject);
        }
    }
}