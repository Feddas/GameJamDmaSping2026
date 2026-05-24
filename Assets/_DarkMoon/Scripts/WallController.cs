using Unity.FPS.AI;
using Unity.FPS.Game;
using UnityEngine;

/// <summary>
/// allows a wall to die. copied some from C:\Users\fedda\Documents\_Shawn\ProjectsUnity\GameJam\GameJamDmaSping2026\Assets\FPS\Scripts\AI\EnemyController.cs
/// </summary>
public class WallController : MonoBehaviour
{
    public GameObject[] WallMeshes;

    [Tooltip("Delay after death where the GameObject is destroyed (to allow for animation)")]
    public float DeathDuration = 0f;

    [Header("VFX")]
    [Tooltip("The VFX prefab spawned when the enemy dies")]
    public GameObject DeathVfx;

    [Tooltip("The point at which the death VFX is spawned")]
    public Transform DeathVfxSpawnPoint;
    public DetectionModule DetectionModule { get; private set; }

    Health m_Health;
    bool m_WasDamagedThisFrame;

    void Start()
    {
        m_Health = GetComponent<Health>();
        DebugUtility.HandleErrorIfNullGetComponent<Health, EnemyController>(m_Health, this, gameObject);

        // Subscribe to damage & death actions
        m_Health.OnDamaged += OnDamaged;
        m_Health.OnDie += OnDie;
    }

    void Update() { }

    void OnDamaged(float damage, GameObject damageSource)
    {
        // test if the damage source is the player
        if (damageSource && !damageSource.GetComponent<EnemyController>())
        {
            // pursue the player
            DetectionModule.OnDamaged(damageSource);

            // play the damage tick sound
            //if (DamageTick && !m_WasDamagedThisFrame)
            //    AudioUtility.CreateSFX(DamageTick, transform.position, AudioUtility.AudioGroups.DamageTick, 0f);

            m_WasDamagedThisFrame = true;
        }
    }

    void OnDie()
    {
        // spawn a particle system when dying
        var vfx = Instantiate(DeathVfx, DeathVfxSpawnPoint.position, Quaternion.identity);
        Destroy(vfx, 5f);

        // this will call the OnDestroy function
        Destroy(gameObject, DeathDuration);
    }
}
