using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    [System.Serializable]
    public class EnemyData
    {
        public float maxHp;
        public float hp;
        public float speed;
        public float attackDamage;
        public bool isDead;
    }
    public EnemyData enemyData;

    public float HP
    {
        get { return enemyData.hp; }

        set
        {
            enemyData.hp = value;

        }

    }
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
        if(!enemyData.isDead)
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

    public bool isHit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnDamage(collision);
    }

    private void OnDamage(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            collision.gameObject.SetActive(false);
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            StartCoroutine(DamageCoroutine(bullet.bulletData.attackDamage));
        }
    }

    public bool isDamage = false;
    IEnumerator DamageCoroutine(float value)
    {
        HP -= value;
        isDamage = true;
        animator.SetBool("isHit", isDamage);

        if (HP <= 0)
        {
            animator.SetTrigger("isDead");
            enemyData.isDead = true;
            GameManager.instance.KillCount++;
        }

        yield return new WaitForSeconds(0.2f);

        if (HP > 0)
        {
            isDamage = false;
            animator.SetBool("isHit", isDamage);
        }

        if (HP <= 0)
        {
            isDamage = false;
            gameObject.SetActive(false);
        }
    }

}
