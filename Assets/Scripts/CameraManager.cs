using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Player player;
    private Rigidbody2D rigid;
    
    private float playerSpeed;
    private Vector2 playerAxis;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 moveDir = rigid.position + player.axis * player.playerData.speed * Time.fixedDeltaTime;
        rigid.MovePosition(moveDir);
    }

}
