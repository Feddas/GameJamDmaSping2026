using UnityEngine;
using Unity.FPS.Game;
using System;

public class SanitySystem : MonoBehaviour
{
    public Health PlayerHealth;

    public SanityLevel CurrentLevel { get; private set; }

    public Action<SanityLevel> OnSanityChanged;

    void Start()
    {
        PlayerHealth.OnDamaged += OnDamaged;
        PlayerHealth.OnHealed += OnHealed;

        EvaluateSanity();
    }

    void OnDamaged(float damage, GameObject source)
    {
        EvaluateSanity();
    }

    void OnHealed(float amount)
    {
        EvaluateSanity();
    }

    void EvaluateSanity()
    {
        float hp = PlayerHealth.CurrentHealth;

        SanityLevel newLevel;

        if (hp < 40)
            newLevel = SanityLevel.VeryInsane;
        else if (hp < 65)
            newLevel = SanityLevel.Insane;
        else if (hp < 90)
            newLevel = SanityLevel.Wary;
        else
            newLevel = SanityLevel.Sane;

        if (newLevel != CurrentLevel)
        {
            CurrentLevel = newLevel;

            OnSanityChanged?.Invoke(CurrentLevel);
        }
    }
}
