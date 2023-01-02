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
    }
    public PlayerData playerData;

    private Rigidbody2D rigid;
    private SpriteRenderer render; 
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GetAxis();
        SpriteFlip();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }


    private Vector2 axis;
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

    private void SpriteFlip()
    {
        if(axis.x!=0)
        {
            render.flipX = axis.x < 0 ? true : false;
        }
    }


}
