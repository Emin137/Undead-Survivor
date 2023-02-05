using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public enum Grade
    {
        Unkmown,
        Rare,
        Boss
    }
        
    [System.Serializable]
    public class EnemyData
    {
        public float maxHp;
        public float hp;
        public float speed;
        public float attackDamage;
        public bool isDead;
        public Grade grade;
        public int index;
        
    }
    public EnemyData enemyData;

    public float HP
    {
        get { return enemyData.hp; }

        set
        {
            enemyData.hp = value;
            if(value<=0) // Enemy Dead
            {
                animator.SetTrigger("isDead");
                enemyData.isDead = true;
                GameManager.instance.KillCount++;
                int rand = Random.Range(0, 2); //50%
                if (enemyData.grade == Grade.Rare)
                    GameManager.instance.poolManager.ItemPooling(1).transform.position = transform.position;
                else
                {
                    if (rand == 0)
                    GameManager.instance.poolManager.ItemPooling(0).transform.position = transform.position;
                }
            }
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

    public bool crit;
    private void OnDamage(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (enemyData.isDead)
                return;
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            float attackDamage = GameManager.instance.player.weaponDatas[GameManager.instance.player.weaponIndex].weaponDamage;
            crit = false;
            int rand = Random.Range(0, 100);
            int critInt = GameManager.instance.player.playerData.crit;
            if (GameManager.instance.player.weaponIndex == 1)
                critInt = critInt + GameManager.instance.player.weaponDatas[1].weaponSpecial;
            if (critInt > rand)
                crit = true;
            if (crit)
                StartCoroutine(DamageCoroutine(attackDamage * GameManager.instance.player.playerData.critdmg / 100));
            else
                StartCoroutine(DamageCoroutine(attackDamage));
            if (bullet.bulletData.penetrate <= 0)
                bullet.gameObject.SetActive(false);
            else
                bullet.bulletData.penetrate--;
        }
    }

    public bool isDamage = false;
    IEnumerator DamageCoroutine(float value)
    {
        HP -= value;
        isDamage = true;
        animator.SetBool("isHit", isDamage);
        Damage damage = GameManager.instance.poolManager.DamagePooling();
        damage.InitDamage(value, transform.position + new Vector3(Random.Range(-0.4f,0.4f), Random.Range(0.5f,0.8f), 0),crit);
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

    public void StageLevelUp()
    {
        enemyData.hp *= 1.2f;
        enemyData.maxHp *= 1.2f;
        enemyData.attackDamage++;
        enemyData.speed *= 1.1f;
    }

    

}
