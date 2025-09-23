using Unity.VisualScripting;
using UnityEngine;

public class musicPlayerScript : MonoBehaviour
{
    [SerializeField] AudioClip mainLayerClip;
    [SerializeField] AudioClip stressLayerClip;
    private AudioSource mainLayer;
    private AudioSource stressLayer;

    private float speed = 1;
    private float speedStressRelation = 0.2f;
    public float stressLevel = 0;
    public float musicVolume; //volume of the music
    void Start()
    {
        //creating the audio source components and playing the music
        mainLayer =  gameObject.AddComponent<AudioSource>();
        stressLayer = gameObject.AddComponent<AudioSource>();
        mainLayer.clip = mainLayerClip;
        stressLayer.clip = stressLayerClip;

        mainLayer.volume = musicVolume;
        stressLayer.volume = stressLevel * musicVolume;

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
