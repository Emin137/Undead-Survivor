using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public List<GameObject> enemyPools = new List<GameObject>();
    public GameObject[] bulletPrefabs;
    public List<GameObject> bulletPools = new List<GameObject>();
    public Transform enemySpawnTrans;
    public Transform bulletSpawnTrans;

    public Enemy EnemyPooling(int index)
    {
        GameObject gameObject = null;

        foreach (var item in enemyPools)
        {
            if(!item.activeSelf)
            {
                gameObject = item;
                gameObject.SetActive(true);
                break;
            }
        }

        if(gameObject==null)
        {
            gameObject = Instantiate(enemyPrefabs[index], enemySpawnTrans);
            enemyPools.Add(gameObject);
        }

        return gameObject.GetComponent<Enemy>();
    }

    public Bullet BulletPooling(int index)
    {
        GameObject gameObject = null;

        foreach (var item in bulletPools)
        {
            if (!item.activeSelf)
            {
                gameObject = item;
                gameObject.SetActive(true);
                break;
            }
        }

        if (gameObject == null)
        {
            gameObject = Instantiate(bulletPrefabs[index], bulletSpawnTrans);
            bulletPools.Add(gameObject);
        }

        return gameObject.GetComponent<Bullet>();
    }
}
