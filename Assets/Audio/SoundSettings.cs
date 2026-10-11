using UnityEngine;
using UnityEngine.Audio;

namespace Korakit
{
    [CreateAssetMenu(menuName = "SoundSettingsPreset", fileName = "SoundSettingsPreset")]
    public class SoundSettings : ScriptableObject
    {
        public AudioMixer AudioMixer;

        [Header("Master")]
        public string MasterName = "Master";
        [Range(-80, 20)]
        public float MasterVolume;

        [Header("Music")]
        public string MusicName = "Music";
        [Range(-80, 20)]
        public float Music;

        [Header("SFX")]
        public string SFXName = "SFX";
        [Range(-80, 20)]
        public float SFX;

    }
}
