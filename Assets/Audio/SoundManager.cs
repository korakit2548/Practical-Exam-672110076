
using UnityEngine;
using UnityEngine.UI;

namespace Korakit
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private SoundSettings m_SoundSettings;

        public Slider m_SliderMasterVolume;
        public Slider m_SliderMusicVolume;
        public Slider m_SliderSFXVolume;

        private const string MasterKey = "MasterVolume";
        private const string MusicKey = "MusicVolume";
        private const string SFXKey = "SFXVolume";

        private void Start()
        {
            if (m_SoundSettings == null ||
                m_SoundSettings.AudioMixer == null)
            {
                Debug.LogError("SoundSettings or AudioMixer is missing!");
                return;
            }

            float master = PlayerPrefs.GetFloat(
                MasterKey, m_SoundSettings.MasterVolume);

            float music = PlayerPrefs.GetFloat(
                MusicKey, m_SoundSettings.Music);

            float sfx = PlayerPrefs.GetFloat(
                SFXKey, m_SoundSettings.SFX);

            SetMasterVolume(master);
            SetMusicVolume(music);
            SetSFXVolume(sfx);
        }

        public void SetMasterVolume(float vol)
        {
            m_SoundSettings.AudioMixer.SetFloat(
                m_SoundSettings.MasterName, vol);

            m_SoundSettings.MasterVolume = vol;
            PlayerPrefs.SetFloat(MasterKey, vol);

            if (m_SliderMasterVolume != null)
                m_SliderMasterVolume.SetValueWithoutNotify(vol);
        }

        public void SetMusicVolume(float vol)
        {
            m_SoundSettings.AudioMixer.SetFloat(
                m_SoundSettings.MusicName, vol);

            m_SoundSettings.Music = vol;
            PlayerPrefs.SetFloat(MusicKey, vol);

            if (m_SliderMusicVolume != null)
                m_SliderMusicVolume.SetValueWithoutNotify(vol);
        }

        public void SetSFXVolume(float vol)
        {
            m_SoundSettings.AudioMixer.SetFloat(
                m_SoundSettings.SFXName, vol);

            m_SoundSettings.SFX = vol;
            PlayerPrefs.SetFloat(SFXKey, vol);

            if (m_SliderSFXVolume != null)
                m_SliderSFXVolume.SetValueWithoutNotify(vol);
        }

        private void OnDisable()
        {
            PlayerPrefs.Save();
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.Save();
        }
    }
}
