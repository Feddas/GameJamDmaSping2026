using Unity.FPS.AI;
using Unity.FPS.Game;
using UnityEngine;

/// <summary>
/// allows a wall to die. copied some from C:\Users\fedda\Documents\_Shawn\ProjectsUnity\GameJam\GameJamDmaSping2026\Assets\FPS\Scripts\AI\EnemyController.cs
/// </summary>
///
[RequireComponent(typeof(WallCombinations))]
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
    bool m_WasDamagedThisFrame;


    Damageable playerDamageable;

    void Start()
    {
        m_Health = GetComponent<Health>();
        DebugUtility.HandleErrorIfNullGetComponent<Health, EnemyController>(m_Health, this, gameObject);

        // Subscribe to damage & death actions
        m_Health.OnDie += OnDie;

        playerDamageable =
        FindObjectOfType<Damageable>();
        pickWallMesh();
    }

    void Update() { }


    private void pickWallMesh()
    {
        if (WallMeshes == null || WallMeshes.Length <= 1)
        {
            return;
        }

        var wallCombinator = this.GetComponent<WallCombinations>();
        wallCombinator.CurrentVisual = (WallCombinationEnum)UnityEngine.Random.Range(0, 3);
    }

    void OnDie()
    {
        if (playerDamageable != null)
        {
            playerDamageable.InflictDamage(
                wallDamageAmount,
                false,
                gameObject
            );
        }

        // spawn a particle system when dying
        var vfx = Instantiate(DeathVfx, DeathVfxSpawnPoint.position, Quaternion.identity);
        Destroy(vfx, 5f);

        // this will call the OnDestroy function
        Destroy(gameObject, DeathDuration);
    }
}
