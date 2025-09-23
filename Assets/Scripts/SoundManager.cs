using UnityEngine;
using UnityEngine.Audio;


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
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    public void CreateSound(AudioClip audioClip, float volume = 0.6f, AudioMixerGroup audioMixerGroup = null)
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
        Destroy(soundToCreate, audioSource.clip.length);
    }
}
