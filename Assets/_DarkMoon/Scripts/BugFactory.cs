using System.Collections;
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
            var newBug = Instantiate(BugPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newBug.transform.parent = this.transform;
        }
    }

    void Update()
    {

    }
}
