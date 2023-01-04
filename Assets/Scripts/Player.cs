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
    }
    public PlayerData playerData;

    private float HP
    {
        get { return playerData.hp; }
        set
        { 
            playerData.hp = value;
        }
    }

    private Rigidbody2D rigid;
    private SpriteRenderer render; 
    private Animator animator;

    public SpriteRenderer weaponRender;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SpriteFlip();
        PlayerAnimation();
        GetAxis();
    }

    private void FixedUpdate()
    {
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
            weaponRender.flipX = axis.x < 0 ? true : false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if (isDamage)
                return;
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            StartCoroutine(OnDamage(enemy.enemyData.attackDamage));
        }
    }

    public bool isDamage=false;
    IEnumerator OnDamage(float value)
    {
        render.color = Color.red;
        HP -= value;
        isDamage=true;
        yield return new WaitForSeconds(0.2f);
        if(HP>0)
        {
            render.color = new Color(1, 1, 1);
            isDamage = false;
        }
        else
        {
            // Dead
        }
    }


}
