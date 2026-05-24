using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Unity.FPS.Game;

public class InsanityPostProcessing : MonoBehaviour
{
    public Health playerHealth;
    public Volume volume;

    private LensDistortion lensDistortion;
    private ChromaticAberration chromatic;
    private Vignette vignette;

    void Start()
    {
        volume.profile.TryGet(out lensDistortion);
        volume.profile.TryGet(out chromatic);
        volume.profile.TryGet(out vignette);

        Debug.Log(lensDistortion);
        Debug.Log(chromatic);
        Debug.Log(vignette);
        lensDistortion.intensity.value = -1f;
        chromatic.intensity.value = 1f;
        vignette.intensity.value = 1f;
    }

    void Update()
    {
        if (playerHealth == null) return;

        float sanity = playerHealth.CurrentHealth;

        // normalize 0–1
        float t = Mathf.InverseLerp(100f, 0f, sanity);

        ApplyEffects(t);
    }

    void ApplyEffects(float t)
    {
        // Wavy distortion
        if (lensDistortion != null)
        {
            Debug.Log("lendistortion get yes");
            lensDistortion.intensity.value = Mathf.Lerp(0f, -0.6f, t);
        }

        // Color split madness
        if (chromatic != null)
        {
            chromatic.intensity.value = Mathf.Lerp(0f, 1f, t);
        }

        // Tunnel vision
        if (vignette != null)
        {
            vignette.intensity.value = Mathf.Lerp(0.2f, 0.6f, t);
        }
    }
}
