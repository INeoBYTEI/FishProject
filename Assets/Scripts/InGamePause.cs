using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEditor;

public class InGamePause : MonoBehaviour
{
    InputAction openMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    void Start()
    {
        openMenu = InputSystem.actions.FindAction("openMenu");
    }
    public void togglePauseMenu()
    {
        if (pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void Update()
    {
        if (openMenu.WasPressedThisFrame())
        {
            togglePauseMenu();
        }
    }
    public void MusicVolumeSliderChanged(Single volumeVal)
    {
        SoundManager.instance.ChangeVolumeOfMixGroup(musicSlider.value, "music");
    }
    public void SFXVolumeSliderChanged(Single volumeVal)
    {
        SoundManager.instance.ChangeVolumeOfMixGroup(SFXSlider.value, "SFX");
    }
    public void toMenuClicked()
    {
        SceneManager.LoadScene("Menu");
    }
    public void backClicked()
    {
        togglePauseMenu();
    }
}
