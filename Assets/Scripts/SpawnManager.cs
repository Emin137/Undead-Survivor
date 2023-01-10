using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public bool enemySpawn=false;
    private void Update()
    {
        if(!enemySpawn)
        {
            StartCoroutine(EnemySpawn());
            enemySpawn = true;
        }
    }

    IEnumerator EnemySpawn()
    {
        Enemy spawnEnemy = GameManager.instance.poolManager.EnemyPooling(Random.Range(0,2));
        spawnEnemy.transform.position = spawnPoints[Random.Range(0, 8)].position;
        spawnEnemy.target = GameManager.instance.player.transform;
        spawnEnemy.enemyData.hp = spawnEnemy.enemyData.maxHp;
        spawnEnemy.enemyData.isDead = false;
        yield return new WaitForSeconds(2f);
        enemySpawn = false;
    }

}
