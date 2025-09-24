using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class musicPlayerScript : MonoBehaviour
{
    [SerializeField] AudioClip mainLayerClip;
    [SerializeField] AudioClip stressLayerClip;
    [SerializeField] AudioMixer mainMix;
    AudioMixerGroup music;
    AudioMixerGroup sound;
    private AudioSource mainLayer;
    private AudioSource stressLayer;

    private float speed = 1;
    [SerializeField] private float speedStressRelation = 0.1f;
    public float stressLevel = 0;
    public float musicVolume; //volume of the music
    void Start()
    {
        music = mainMix.FindMatchingGroups("Music")[0];
        sound = mainMix.FindMatchingGroups("SFX")[0];
        //creating the audio source components and playing the music
        mainLayer =  gameObject.AddComponent<AudioSource>();
        stressLayer = gameObject.AddComponent<AudioSource>();
        mainLayer.clip = mainLayerClip;
        stressLayer.clip = stressLayerClip;

        mainLayer.volume = musicVolume;
        stressLayer.volume = stressLevel * musicVolume;

        mainLayer.outputAudioMixerGroup = music;
        stressLayer.outputAudioMixerGroup = sound;

        mainLayer.Play();
        stressLayer.Play();
    }
    void Update()
    {

        speed = 1 + (stressLevel * speedStressRelation);
        mainLayer.pitch = speed;
        stressLayer.pitch = speed;

        stressLayer.volume = stressLevel * musicVolume;
        if (!mainLayer.isPlaying)
        {
            mainLayer.Stop();
            stressLayer.Stop();
            mainLayer.time = 0;
            stressLayer.time = 0;
            mainLayer.Play();
            stressLayer.Play();
        }
    }
}
