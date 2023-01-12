using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public bool enemySpawn=false;
    public float spawnTime;

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
        InitEnemy(spawnEnemy);
        yield return new WaitForSeconds(spawnTime);
        enemySpawn = false;
    }

    private void InitEnemy(Enemy enemy)
    {
        enemy.enemyData.isDead = false;
        enemy.enemyData.hp=enemy.enemyData.maxHp;
    }

    public void ItemSpawn()
    {
        Item item =  GameManager.instance.poolManager.ItemPooling(Random.Range(1, 3));
        item.transform.position = spawnPoints[Random.Range(0, 8)].position;
    }

}
