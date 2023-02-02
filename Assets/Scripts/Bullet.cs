using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [System.Serializable]
    public class BulletData
    {
        public float speed;
        public float attackDamage;
    }
    public BulletData bulletData;

    public Rigidbody2D rigid;

    public Vector2 direction;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if ((transform.position - GameManager.instance.player.transform.position).magnitude > 50)
            gameObject.SetActive(false);
    }

    public void SetForce(Vector2 direction)
    {
        rigid.AddForce(direction*bulletData.speed, ForceMode2D.Impulse);
    }
}
