using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public List<GameObject> enemyPools = new List<GameObject>();

    public GameObject[] bulletPrefabs;
    public List<GameObject> bulletPools = new List<GameObject>();

    public GameObject[] itemPrefabs;
    public List<GameObject> itemPools = new List<GameObject>();

    public GameObject damagePrefabs;
    public List<GameObject> damagePools = new List<GameObject>();

    public Transform enemySpawnTrans;
    public Transform bulletSpawnTrans;
    public Transform itemSpawnTrans;
    public RectTransform damageTrans;


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

    public Item ItemPooling(int index)
    {
        GameObject gameObject = null;

        foreach (var item in itemPools)
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
            gameObject = Instantiate(itemPrefabs[index], itemSpawnTrans);
            itemPools.Add(gameObject);
        }

        return gameObject.GetComponent<Item>();
    }

    public Damage DamagePooling()
    {
        GameObject gameObject = null;

        foreach (var item in damagePools)
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
            gameObject = Instantiate(damagePrefabs, damageTrans);
            damagePools.Add(gameObject);
        }

        return gameObject.GetComponent<Damage>();
    }
}
