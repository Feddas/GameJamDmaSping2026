using System.Collections;
using System.Linq;
using UnityEngine;

public class BugFactory : MonoBehaviour
{
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
            BoxCollider roomToSpawn = Floors[roomIndex].GetComponent<BoxCollider>();
            var newBug = Instantiate(BugPrefab, GetRandomPoint(roomToSpawn), Quaternion.identity);
            newBug.transform.parent = this.transform;

            // next iteration
            BugsLeft--;
            roomIndex = (roomIndex + 1) % Floors.Count();
        }
    }

    void Update()
    {

    }

    public Vector3 GetRandomPoint(BoxCollider boxCollider, int atFloor = 1)
    {
        Bounds bounds = boxCollider.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            atFloor,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
