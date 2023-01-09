using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    [System.Serializable]
    public class EnemyData
    {
        public float hp;
        public float speed;
        public float attackDamage;
    }
    public EnemyData enemyData;

    private Rigidbody2D rigid;
    private SpriteRenderer render;
    private Animator animator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GetAxis();
        SpriteFlip();
    }

    private void FixedUpdate()
    {
        EnemyMove();
    }

    public Vector2 axis;
    private void GetAxis()
    {
        Vector2 offset = target.position - transform.position;
        axis = offset.normalized;
    }

    private Vector2 moveDir;
    private void EnemyMove()
    {
        moveDir = rigid.position + axis * enemyData.speed * Time.fixedDeltaTime;
        rigid.MovePosition(moveDir);
    }

    private void SpriteFlip()
    {
        if (axis.x != 0)
        {
            render.flipX = axis.x < 0 ? true : false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            collision.gameObject.SetActive(false);
        }
    }

}
