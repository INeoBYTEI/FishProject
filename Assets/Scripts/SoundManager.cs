using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;


/****************
 * //ATTENTION! *
 ****************/
/*************************************************************************************************
 * //THE WAY YOU WOULD USE THIS IN ANY SCRIPT IS JUST TYPING SOUNDMANAGER.INSTANCE.CREATESOUND() *
 *                        // AND THEN PUT IN A REFERENCE TO AN AUDIOCLIP                         *
 *************************************************************************************************/

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] AudioMixer mainMix;
    public AudioMixerGroup SFXMixGroup;
    public AudioMixerGroup MusicMixGroup;
    void Start()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SFXMixGroup = mainMix.FindMatchingGroups("Music")[0];
            MusicMixGroup = mainMix.FindMatchingGroups("SFX")[0];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreateSound(AudioClip audioClip, float volume = 0.6f, AudioMixerGroup audioMixerGroup = null, Vector3 position = new Vector3())
    {
        GameObject soundToCreate = new GameObject("soundEffect");
        soundToCreate.SetActive(false);
        AudioSource audioSource = soundToCreate.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        if (audioMixerGroup)
        {
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }
        soundToCreate.SetActive(true);
        soundToCreate.transform.position = position;
        Destroy(soundToCreate, audioSource.clip.length);
    }
    public void ChangeVolumeOfMixGroup(float volume, string mixGroup)
    {
        if (mixGroup == "music") {
            mainMix.SetFloat("MusicVol", volume);
        }
        else if (mixGroup == "SFX") {
            mainMix.SetFloat("SFXVol", volume);
        }
        
    }
}
