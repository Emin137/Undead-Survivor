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
    public Transform damageTrans;

    private void Start()
    {
        InitEnemies();
    }


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
            if (!item.activeSelf && item.GetComponent<Item>().itemData.index == index)
            {
                gameObject = item;
                gameObject.SetActive(true);
                item.GetComponent<Item>().isMagnet = false;
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

    public void InitEnemies()
    {
        Enemy enemy = enemyPrefabs[0].GetComponent<Enemy>();
        enemy.enemyData.maxHp = 10f;
        enemy.enemyData.hp = 10f;
        enemy.enemyData.attackDamage = 1f;
        enemy.enemyData.speed = 3f;
        enemy = enemyPrefabs[1].GetComponent<Enemy>();
        enemy.enemyData.maxHp = 20f;
        enemy.enemyData.hp = 20f;
        enemy.enemyData.attackDamage = 2f;
        enemy.enemyData.speed = 2f;
    }
}
