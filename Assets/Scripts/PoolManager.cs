using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public List<GameObject>[] enemyPools;
    public Transform spawnTrans;

    private void Awake()
    {
        enemyPools = new List<GameObject>[enemyPrefabs.Length];

        for (int i = 0; i < enemyPools.Length; i++)
        {
            enemyPools[i] = new List<GameObject>();
        }
    }

    public Enemy EnemyPooling(int index)
    {
        GameObject gameObject = null;

        foreach (var item in enemyPools[index])
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
            gameObject = Instantiate(enemyPrefabs[index], spawnTrans);
            enemyPools[index].Add(gameObject);
        }

        return gameObject.GetComponent<Enemy>();
    }
}
