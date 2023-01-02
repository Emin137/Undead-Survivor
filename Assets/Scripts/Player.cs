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

    private Rigidbody2D rb;
    private SpriteRenderer sr; 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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
        axis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    
    private void PlayerMove()
    {
        Vector2 vector2 = rb.position + axis * playerData.speed * Time.fixedDeltaTime;
        rb.MovePosition(vector2);
    }

    private void SpriteFlip()
    {
        if(axis.x!=0)
        {
            sr.flipX = axis.x < 0 ? true : false;
        }
    }


}
