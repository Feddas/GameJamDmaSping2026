using UnityEngine;

public class SanityBootstrap : MonoBehaviour
{
    public SanitySystem SanitySystem;
    public SanityAudioManager AudioManager;

    void Start()
    {
        SanitySystem.OnSanityChanged +=
            AudioManager.SetSanity;
    }
}
