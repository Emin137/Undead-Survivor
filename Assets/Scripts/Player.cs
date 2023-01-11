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
        public float attackRange;
        public float attackSpeed;
        public bool isDead = false;
    }
    public PlayerData playerData;

    public float HP
    {
        get { return playerData.hp; }
        set
        { 
            playerData.hp = value;
            GameManager.instance.uiManager.SetHp(value);
        }
    }

    public float EXP
    {
        get { return playerData.exp; }
        set
        {
            playerData.exp = value;
            GameManager.instance.uiManager.SetExp(value);
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

    private float timer;
    private void Update()
    {
        if (playerData.isDead)
            return;
        SpriteFlip();
        PlayerAnimation();
        GetAxis();
        FindTarget();
        timer += Time.deltaTime;
        if (timer > playerData.attackSpeed && target)
        {
            Fire();
            timer = 0f;
        }
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnDamage(collision);
        OnItem(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnDamage(collision);
    }

    private void OnItem(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);
            Item item = collision.gameObject.GetComponent<Item>();
            item.isMagnet = false;
            switch (item.itemData.Type)
            {
                case Item.ItemType.Exp:
                    EXP += item.itemData.value;
                    break;
                case Item.ItemType.Heal:
                    break;
                case Item.ItemType.Magnet:
                    Magnet();
                    break;
                default:
                    break;
            }
        }
    }

    private void Magnet()
    {
        foreach (var item in GameManager.instance.poolManager.itemPools)
        {
            Item item1 = item.GetComponent<Item>();
            if(item1.itemData.Type != Item.ItemType.Magnet)
            {
                item1.isMagnet = true;
            }

        }
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
    public void FindTarget()
    {
        target = null;
        float min = float.MaxValue;
        foreach (var item in GameManager.instance.poolManager.enemyPools)
        {
            float mOffset = (item.transform.position - transform.position).magnitude;
            if(mOffset<min && mOffset<playerData.attackRange)
            {
                if (!item.GetComponent<Enemy>().enemyData.isDead)
                {
                    min = mOffset;
                    target = item.transform;
                    GameManager.instance.weapon.target = target;
                }
            }
        }
    }

    private void Fire()
    {
        Bullet bullet =  GameManager.instance.poolManager.BulletPooling(0);
        bullet.transform.position = transform.position;
        bullet.SetForce((target.position - transform.position).normalized);
    }

   
}
