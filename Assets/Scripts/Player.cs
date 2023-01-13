using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public float maxHp;
        public float hp;
        public float speed;
        public int level;
        public float maxExp;
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
            if(value>0) // 데미지를 입을때
            {
                Damage damage =  GameManager.instance.poolManager.DamagePooling();
                damage.InitHp(value-playerData.hp,transform.position+new Vector3(0,0.5f));
            }
            else // 플레이어 사망시
            {
                animator.SetTrigger("isDead");
                weaponRender.gameObject.SetActive(false);
                playerData.isDead = true;
                rigid.bodyType = RigidbodyType2D.Static;
                GameManager.instance.uiManager.imageDead.gameObject.SetActive(true);
                Time.timeScale = 0f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            if(value>playerData.maxHp) // 최대체력을 넘었을때 제한
                value=playerData.maxHp;

            playerData.hp = value;
            GameManager.instance.uiManager.SetHP(value);
        }
    }

    public float EXP
    {
        get { return playerData.exp; }
        set
        {
            playerData.exp = value;
            GameManager.instance.uiManager.SetExp(value);

            if(value>=playerData.maxExp) // 레벨업
            {
                playerData.level++;
                playerData.exp -= playerData.maxExp;
                playerData.maxExp *= 1.5f;
                GameManager.instance.uiManager.SetLevel(playerData.exp,playerData.maxExp,playerData.level);
            }
        }
    }

    private Rigidbody2D rigid;
    private SpriteRenderer render; 
    private Animator animator;

    public Animator effectAnimator;
    public SpriteRenderer weaponRender;
    public Transform bulletSpawnTrans;

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
        CollisionEnemy(collision);
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CollisionEnemy(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionItem(collision);
    }

    private void CollisionItem(Collider2D collision) // 아이템 충돌시
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
                    HP += item.itemData.value;
                    break;
                case Item.ItemType.Magnet:
                    TriggerMagnet();
                    break;
                default:
                    break;
            }
        }
    }

    private void TriggerMagnet()
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

    private void CollisionEnemy(Collision2D collision) // 적 충돌시
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
        isDamage = true;

        yield return new WaitForSeconds(0.5f);

        render.color = new Color(1, 1, 1);
        isDamage = false;
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
        bullet.transform.position = bulletSpawnTrans.position;
        bullet.SetForce((target.position - transform.position).normalized);
        effectAnimator.Play("FireEffect");
    }

   
}
