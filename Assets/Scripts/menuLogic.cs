using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuLogic : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Animator fadeToBlackAnimator;
    [SerializeField] GameObject optionsMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        optionsMenu.SetActive(false);
    }
    public void StartClicked()
    {
        fadeToBlackAnimator.Play("fadeToBlack");
    }
    public void OptionsClicked()
    {
        optionsMenu.SetActive(true);
    }
    public void OptionsBackClicked()
    {
        optionsMenu.SetActive(false);
    }
    public void QuitClicked()
    {
        fadeToBlackAnimator.Play("fadeToBlackQuit");
    }
    public void goToGameScene()
    {
        SceneManager.LoadScene("game"); //byt ut till faktiska namnet på spelscenen
    }
    public void MusicVolumeSliderChanged(Single volumeVal)
    {
        SoundManager.instance.ChangeVolumeOfMixGroup(musicSlider.value, "music");
    }
    public void SFXVolumeSliderChanged(Single volumeVal)
    {
        SoundManager.instance.ChangeVolumeOfMixGroup(SFXSlider.value, "SFX");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
