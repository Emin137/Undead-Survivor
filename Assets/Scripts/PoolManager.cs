using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public List<GameObject>[] pools;
    public Transform spawnTrans;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public Enemy Pooling(int index)
    {
        GameObject gameObject = null;

        foreach (var item in pools[index])
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
            gameObject = Instantiate(prefabs[index], spawnTrans);
            pools[index].Add(gameObject);
        }

        return gameObject.GetComponent<Enemy>();
    }
}
