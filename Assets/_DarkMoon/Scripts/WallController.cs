using Unity.FPS.Game;
using UnityEngine;

/// <summary>
/// allows a wall to die. copied some from C:\Users\fedda\Documents\_Shawn\ProjectsUnity\GameJam\GameJamDmaSping2026\Assets\FPS\Scripts\AI\EnemyController.cs
/// </summary>
[RequireComponent(typeof(Health))]
public class WallController : MonoBehaviour
{
    public GameObject[] WallMeshes;

    [Tooltip("Delay after death where the GameObject is destroyed (to allow for animation)")]
    public float DeathDuration = 0f;

    public float wallDamageAmount = 10f;

    [Header("VFX")]
    [Tooltip("The VFX prefab spawned when the enemy dies")]
    public GameObject DeathVfx;

    [Tooltip("The point at which the death VFX is spawned")]
    public Transform DeathVfxSpawnPoint;

    Health m_Health;
    Damageable playerDamageable;

    void Awake()
    {
        m_Health = GetComponent<Health>();
        DebugUtility.HandleErrorIfNullGetComponent<Health, WallController>(m_Health, this, gameObject);

        playerDamageable = FindAnyObjectByType<Damageable>();
    }

    void OnEnable()
    {
        if (!m_Health)
        {
            m_Health = GetComponent<Health>();
        }

        if (!m_Health)
        {
            return;
        }

        // Subscribe to damage & death actions
        m_Health.OnDamaged -= OnDamaged;
        m_Health.OnDamaged += OnDamaged;
        m_Health.OnDie -= OnDie;
        m_Health.OnDie += OnDie;
    }

    void OnDisable()
    {
        if (!m_Health)
        {
            return;
        }

        m_Health.OnDamaged -= OnDamaged;
        m_Health.OnDie -= OnDie;
    }

    void OnDamaged(float damage, GameObject damageSource)
    {
        // Intentionally left blank: walls don't require AI detection behavior.
    }

    void OnDie()
    {
        if (!playerDamageable)
        {
            playerDamageable = FindAnyObjectByType<Damageable>();
        }

        if (playerDamageable != null)
        {
            playerDamageable.InflictDamage(
                wallDamageAmount,
                false,
                gameObject
            );
        }

        // spawn a particle system when dying
        if (!DeathVfx)
        {
            Debug.LogWarning($"WallController on '{name}' has no DeathVfx assigned.", this);
        }
        else
        {
            if (!DeathVfxSpawnPoint)
            {
                Debug.LogWarning($"WallController on '{name}' has no DeathVfxSpawnPoint. Using wall position.", this);
            }
            var spawnPoint = DeathVfxSpawnPoint ? DeathVfxSpawnPoint.position : transform.position;
            var vfx = Instantiate(DeathVfx, spawnPoint, Quaternion.identity);
            Destroy(vfx, 5f);
        }

        // this will call the OnDestroy function
        Destroy(gameObject, DeathDuration);
    }
}
