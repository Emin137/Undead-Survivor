using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public bool isSpawn=false;
    private void Update()
    {
        if(!isSpawn)
        {
            StartCoroutine(Spawn());
            isSpawn = true;
        }
    }

    IEnumerator Spawn()
    {
        Enemy spawnEnemy = GameManager.instance.poolManager.Pooling(Random.Range(0,2));
        spawnEnemy.transform.position = spawnPoints[Random.Range(0, 8)].position;
        spawnEnemy.target = GameManager.instance.player.transform;
        yield return new WaitForSeconds(2f);
        isSpawn = false;
    }
}
