
using UnityEngine;
using UnityEngine.Audio;

namespace Korakit
{
    public class InteractCollectible : MonoBehaviour
    {
        public ItemType itemType;

        [Header("Sound")]
        [SerializeField] private AudioClip collectSound;
        [SerializeField] private AudioMixerGroup sfxGroup;

        private PlayerController nearbyPlayer;

        private void OnTriggerEnter(Collider other)
        {
            PlayerController player =
                other.GetComponentInParent<PlayerController>();

            if (player == null)
                return;

            nearbyPlayer = player;
            nearbyPlayer.SetNearbyItem(this);
        }

        private void OnTriggerExit(Collider other)
        {
            PlayerController player =
                other.GetComponentInParent<PlayerController>();

            if (player == null || player != nearbyPlayer)
                return;

            player.SetNearbyItem(null);
            nearbyPlayer = null;
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

            PlayCollectSound();

            nearbyPlayer.SetNearbyItem(null);
            nearbyPlayer = null;

            Destroy(gameObject);
        }

        private void PlayCollectSound()
        {
            if (collectSound == null)
                return;

            GameObject soundObject =
                new GameObject("CollectibleSound");

            AudioSource source =
                soundObject.AddComponent<AudioSource>();

            source.clip = collectSound;
            source.outputAudioMixerGroup = sfxGroup;
            source.spatialBlend = 0f;
            source.Play();

            Destroy(soundObject, collectSound.length);
        }
    }
}
