using UnityEngine;

public class musicPlayerScript : MonoBehaviour
{
    [SerializeField] AudioSource mainLayer;
    [SerializeField] AudioSource stressLayer;

    void Start()
    {
        mainLayer.Play();
        stressLayer.Play();
    }
    void Update()
    {
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
