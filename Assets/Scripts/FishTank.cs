using UnityEngine;

public class FishTank : MonoBehaviour
{
    public GameObject fishPrefab;
    private Renderer tankWaterMaterial;

    [Header("Tank Stats")]
    public int Hunger = 0;
    public int Ammonia = 0;
    public int Population = 0;

    [Header("Tank Limits")]
    public int MaxHunger = 100;
    public int MaxAmmonia = 100;
    public int MaxPopulation = 100;

    private void Start()
    {
        tankWaterMaterial = this.GetComponentInChildren<Renderer>();
    }
    private void Update()
    {
        // Update water color based on Ammonia level
        float ammoniaRatio = Mathf.Clamp01((float)Ammonia / MaxAmmonia);
        Color cleanColor = Color.cyan;
        Color dirtyColor = Color.green;
        tankWaterMaterial.material.color = Color.Lerp(cleanColor, dirtyColor, ammoniaRatio);
    }
}
