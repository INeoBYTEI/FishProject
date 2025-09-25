using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishTank : MonoBehaviour
{
    public GameObject fishPrefab;
    public GameObject snails;
    public Slider ammoniaSlider;
    public Slider hungerSlider;
    public TextMeshProUGUI populationText;
    public Renderer tankWaterMaterial;
    public Renderer tankGlassMaterial;
    public Transform fishSpawnPoint;

    [Header("Tank Stats")]
    public float hunger = 0;
    public float ammonia = 0;
    public float dirtiness = 0;
    public int population = 0;
    public float points = 0;
    public float startTime = 0f;
    public List<GameObject> fishList = new List<GameObject>();

    [Header("Tank Limits")]
    public int maxHunger = 100;
    public int maxAmmonia = 100;
    public int maxDirtiness = 100;
    public int maxPopulation = 100;

    [Header("Tank Effects")]
    public bool hasSnails = false;
    public bool isRaging = false;

    private float timer = 0f;
    private float explosionTimer = 10f;


    Color cleanColor = Color.cyan;
    Color toxicColor = Color.yellow;
    Color cleanGlassColor = Color.white;
    Color dirtyGlassColor = Color.green;

    private void Start()
    {
        cleanColor.a = 0.5f; // Semi-transparent
        toxicColor.a = 0.75f; // More opaque
        cleanGlassColor.a = 0.2f;
        dirtyGlassColor.a = 1f;
    }
    private void Update()
    {
        // Update water color based on Ammonia level
        ammoniaSlider.value = ammonia / maxAmmonia;
        hungerSlider.value = hunger / maxHunger;
        populationText.text = (float)population + "/" + maxPopulation;
        float popRatio = Mathf.Clamp01((float)population / maxPopulation);
        populationText.color = Color.Lerp(Color.white, Color.red, popRatio);
        float ammoniaRatio = Mathf.Clamp01(ammonia / (maxAmmonia + 15));
        tankWaterMaterial.material.color = Color.Lerp(cleanColor, toxicColor, ammoniaRatio);
        // float dirtinessRatio = Mathf.Clamp01(dirtiness / maxDirtiness);
        // tankGlassMaterial.material.color = Color.Lerp(cleanGlassColor, dirtyGlassColor, dirtinessRatio);
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            UpdateTank();
            timer = 0f;
        }

        if (startTime > 0)
        {
            startTime -= Time.deltaTime;
        }
        else if (startTime != -100f)
        {
            AddFish(fishSpawnPoint);
            AddFish(fishSpawnPoint);
            startTime = -100f;
        }
    }

    public void AddFish(Transform spawnPoint)
    {
        if (population < maxPopulation)
        {
            GameObject fish = Instantiate(fishPrefab, spawnPoint.position, Quaternion.identity);
            fish.transform.parent = this.gameObject.transform;
            fishList.Add(fish);
            population = fishList.Count;
        }
    }

    public void RemoveFish(GameObject fish = null)
    {
        if (population > 0 && fishList.Contains(fish))
        {
            if (fish == null)
            {
                fish = fishList[fishList.Count - 1];
            }
            fishList.Remove(fish);
            Destroy(fish);
            population = fishList.Count;
        }
    }

    public void FeedFish(int amount)
    {
        points += 5;
        hunger = Mathf.Max(hunger - amount, 0);
    }

    public void DetoxifyTank(int amount)
    {
        points += 5;
        ammonia = Mathf.Max(ammonia - amount, 0);
    }

    public void RageFish()
    {
        // Implement rage effect on fish if needed
        points += 5;
        isRaging = true;
        Invoke("EndRage", 10f); // Rage lasts for 10 seconds
    }

    void EndRage()
    {
        isRaging = false;
    }

    public void ToggleSnails()
    {
        hasSnails = !hasSnails;
        snails.SetActive(hasSnails);
    }

    void UpdateTank()
    {
        if (population > 0)
        {
            hunger = Mathf.Min(hunger + (float)population * 0.035f, maxHunger);
            if (hasSnails)
            {
                if (ammonia > 0)
                {
                    ammonia -= maxAmmonia * 0.01f;
                }
                ammonia = Mathf.Min(ammonia + (float)population * 0.025f, maxAmmonia);
            }
            else
            {
                ammonia = Mathf.Min(ammonia + (float)population * 0.025f, maxAmmonia);
            }

            if (ammonia >= maxAmmonia)
            {
                for (int i = 0; i < 5; i++)
                {
                    RemoveFish();
                }
            }
            else if (ammonia > maxAmmonia * .5f)
            {
                int removeCount = Mathf.RoundToInt((ammonia - maxAmmonia * 0.5f) * 0.05f);
                if (removeCount > 0)
                {
                    for (int i = 0; i < removeCount; i++)
                    {
                        RemoveFish();
                    }
                }
            }

            if (isRaging)
            {
                population = Mathf.Max(population - 5, 0);
                hunger = Mathf.Max(hunger - 5, 0);
            }
            else if (hunger >= maxHunger)
            {
                RageFish();
            }
        }
        points += population;
    }
    
    public void ExplodeTank()
    {
        // Implement tank explosion logic here
        Debug.Log("Tank has exploded due to extreme conditions!");
    }
}
