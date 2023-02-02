using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float rotateSpeed;
    [System.NonSerialized]
    public SpriteRenderer render;
    [System.NonSerialized]
    public Transform target;
    public Transform fireTrans;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    public float angle;
    private void Update()
    {
        target = GameManager.instance.player.target;
        if (!target)
        {
            transform.rotation = Quaternion.identity;
            render.flipX = GameManager.instance.player.axis.x < 0 ? true : false;
            render.flipY = false;
            GameManager.instance.player.effectAnimator.Play("None");
            return;
        }
        render.flipX = false;
        Vector2 targetPos = target.position;
        Vector2 myPos = transform.position;
        Vector2 offset = targetPos - myPos;

        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        if(offset.x<0)
        {
            render.flipY = true;
        }
        else
        {
            render.flipY = false;
        }
    }
}
