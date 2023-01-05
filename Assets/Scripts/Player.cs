using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public float hp;
        public float speed;
        public int level;
        public float exp;
        public float attackDamage;
        public bool isDead = false;
    }
    public PlayerData playerData;

    private float HP
    {
        get { return playerData.hp; }
        set
        { 
            playerData.hp = value;
            GameManager.instance.uiManager.SetHp(value);
        }
    }

    private Rigidbody2D rigid;
    private SpriteRenderer render; 
    private Animator animator;

    public SpriteRenderer weaponRender;
    public Transform bulletSpawnerTrans;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerData.isDead)
            return;
        SpriteFlip();
        PlayerAnimation();
        GetAxis();
    }

    private void FixedUpdate()
    {
        if (playerData.isDead)
            return;
        PlayerMove();
    }


    public Vector2 axis;
    private void GetAxis()
    {
        if (GameManager.instance.joystickManager.isDrag)
        {
            axis = GameManager.instance.joystickManager.axis;
        }
        else
        {
            axis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }
    }

    private Vector2 moveDir;
    private void PlayerMove()
    {
        moveDir = rigid.position + axis * playerData.speed * Time.fixedDeltaTime;
        rigid.MovePosition(moveDir);
    }

    private bool isMove;
    private void PlayerAnimation()
    {
        isMove = axis.magnitude > 0 ? true : false;
        animator.SetBool("isMove", isMove);
    }

    private void SpriteFlip()
    {
        if(axis.x!=0)
        {
            render.flipX = axis.x < 0 ? true : false;
            bulletSpawnerTrans.position = axis.x < 0 ? new Vector2(-0.8f, 0.14f) : new Vector2(0.8f, 0.14f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnDamage(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnDamage(collision);
    }

    private void OnDamage(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isDamage)
                return;
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            StartCoroutine(DamageCoroutine(enemy.enemyData.attackDamage));
        }
    }

    public bool isDamage=false;
    IEnumerator DamageCoroutine(float value)
    {
        render.color = Color.red;
        HP -= value;
        isDamage=true;

        if(HP<=0)
        {
            render.color = new Color(1, 1, 1);
            animator.SetTrigger("isDead");
            weaponRender.gameObject.SetActive(false);
            playerData.isDead = true;
            rigid.bodyType = RigidbodyType2D.Static;
        }

        yield return new WaitForSeconds(0.5f);

        if(HP>0)
        {
            render.color = new Color(1, 1, 1);
            isDamage = false;
        }
    }
    public List<Enemy> enemies = new List<Enemy>();
    public Transform target;
    private void OnAttack()
    {
        Vector2 targetPos;
        float minDistance = float.MaxValue;
        float offset;
        if (target)
        {

        }
        else
        {
            for (int i = 0; i < GameManager.instance.poolManager.enemyPools.Length; i++)
            {
                for (int j = 0; j < GameManager.instance.poolManager.enemyPools[i].Count; j++)
                {
                    Enemy enemy = GameManager.instance.poolManager.enemyPools[i][j].GetComponent<Enemy>();
                    targetPos = enemy.transform.position;
                    Vector2 playerPos = transform.position;
                    offset = (playerPos - targetPos).magnitude;
                    if (offset < minDistance)
                    {
                        minDistance = offset;
                        target = enemy.transform;
                    }

                }
            }
        }
    }

    private void FindEnemy()
    {

    }



}
