using System.Collections;
using System.Linq;
using UnityEngine;

public class BugFactory : MonoBehaviour
{
    public bool spawnAtZero = false;
    public Transform[] Floors;
    public float SecondsNextBugSpawn = 60;
    public float BugsLeft = 10;
    public GameObject BugPrefab;

    private int roomIndex = 0;

    void Start()
    {
        StartCoroutine(SpawnBug());
    }

    IEnumerator SpawnBug()
    {
        while (BugsLeft-- > 0)
        {
            yield return new WaitForSeconds(SecondsNextBugSpawn);

            // make bug
            var spawnLocation = determineSpawnLocation();
            var newBug = Instantiate(BugPrefab, spawnLocation, Quaternion.identity);
            newBug.transform.parent = this.transform;

            // next iteration
            BugsLeft--;
            roomIndex = (roomIndex + 1) % Floors.Count();
        }
    }

    void Update()
    {

    }

    public Vector3 determineSpawnLocation()
    {
        if (Floors == null || Floors.Length == 0 || spawnAtZero)
        {
            return Vector3.zero;
        }

        BoxCollider roomToSpawn = Floors[roomIndex].GetComponent<BoxCollider>();
        return GetRandomLocation(roomToSpawn);
    }

    public Vector3 GetRandomLocation(BoxCollider boxCollider, int atFloor = 1)
    {
        Bounds bounds = boxCollider.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            atFloor,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
