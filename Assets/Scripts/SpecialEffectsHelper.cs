using UnityEngine;

/// <summary>
/// Creating instance of particles from code with no effort
/// </summary>
public class SpecialEffectsHelper : MonoBehaviour
{
    /// <summary>
    /// Singleton
    /// </summary>
    public static SpecialEffectsHelper Instance;

    [SerializeField] private ParticleSystem fireEffect;

    private void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SpecialEffectsHelper!");
        }

        Instance = this;
    }

    /// <summary>
    /// Create an explosion at the given location
    /// </summary>
    /// <param name="position"></param>
    public void Explosion(Vector3 position)
    {
        Instantiate(fireEffect, position);
    }

    private static void Instantiate(ParticleSystem prefab, Vector3 position)
    {
        var newParticleSystem = Instantiate(
            prefab,
            position,
            Quaternion.identity
        );

        Destroy(
            newParticleSystem.gameObject,
            newParticleSystem.main.startLifetime.constant
        );
    }
}