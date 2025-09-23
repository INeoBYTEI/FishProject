using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishTank : MonoBehaviour
{
    public GameObject fishPrefab;
    public Slider ammoniaSlider;
    public Slider hungerSlider;
    public TextMeshProUGUI populationText;
    public Renderer tankWaterMaterial;

    [Header("Tank Stats")]
    public int Hunger = 0;
    public int Ammonia = 0;
    public int Population = 0;

    [Header("Tank Limits")]
    public int MaxHunger = 100;
    public int MaxAmmonia = 100;
    public int MaxPopulation = 100;

    
    Color cleanColor = Color.cyan;
    Color toxicColor = Color.yellow;

    private void Start()
    {
        cleanColor.a = 0.25f; // Semi-transparent
        toxicColor.a = 0.3f; // More opaque
    }
    private void Update()
    {
        // Update water color based on Ammonia level
        ammoniaSlider.value = (float)Ammonia / MaxAmmonia; // Slightly exceed max for visual effect
        hungerSlider.value = (float)Hunger / MaxHunger;
        populationText.text = Population + "/" + MaxPopulation;
        float popRatio = Mathf.Clamp01((float)Population / MaxPopulation);
        populationText.color = Color.Lerp(Color.white, Color.red, popRatio);
        float ammoniaRatio = Mathf.Clamp01((float)Ammonia / (MaxAmmonia + 25));
        tankWaterMaterial.material.color = Color.Lerp(cleanColor, toxicColor, ammoniaRatio);
    }
}
