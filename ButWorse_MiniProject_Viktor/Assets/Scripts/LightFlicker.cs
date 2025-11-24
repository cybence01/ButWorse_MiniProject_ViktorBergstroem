using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [Header("Flicker Settings")]
    [Tooltip("The light component to flicker")]
    public Light lightToFlicker;
    
    [Tooltip("Base intensity of the light")]
    public float baseIntensity = 1f;
    
    [Tooltip("How much the intensity can vary")]
    [Range(0f, 1f)]
    public float flickerAmount = 0.3f;
    
    [Tooltip("Speed of the flicker effect")]
    public float flickerSpeed = 10f;
    
    [Header("Random Flicker")]
    [Tooltip("Enable random flickering intervals")]
    public bool useRandomFlicker = false;
    
    [Tooltip("Minimum time between flickers")]
    public float minFlickerDelay = 0.05f;
    
    [Tooltip("Maximum time between flickers")]
    public float maxFlickerDelay = 0.2f;
    
    private float nextFlickerTime;
    
    void Start()
    {
        // If no light is assigned, try to get the Light component on this GameObject
        if (lightToFlicker == null)
        {
            lightToFlicker = GetComponent<Light>();
            
            if (lightToFlicker == null)
            {
                Debug.LogError("No Light component found! Please assign a light to flicker.");
                enabled = false;
                return;
            }
        }
        
        baseIntensity = lightToFlicker.intensity;
        nextFlickerTime = Time.time;
    }
    
    void Update()
    {
        if (lightToFlicker == null) return;
        
        if (useRandomFlicker)
        {
            RandomFlicker();
        }
        else
        {
            SmoothFlicker();
        }
    }
    
    void SmoothFlicker()
    {
        // Use Perlin noise for smooth, natural-looking flicker
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0f);
        float intensity = baseIntensity + (noise - 0.5f) * flickerAmount * 2f;
        lightToFlicker.intensity = Mathf.Max(0f, intensity);
    }
    
    void RandomFlicker()
    {
        if (Time.time >= nextFlickerTime)
        {
            // Random intensity change
            float randomIntensity = baseIntensity + Random.Range(-flickerAmount, flickerAmount);
            lightToFlicker.intensity = Mathf.Max(0f, randomIntensity);
            
            // Set next flicker time
            nextFlickerTime = Time.time + Random.Range(minFlickerDelay, maxFlickerDelay);
        }
    }
}