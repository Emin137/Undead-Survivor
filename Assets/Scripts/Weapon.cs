using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float rotateSpeed;
    public Transform target;
    public SpriteRenderer render;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 myPos = transform.position;
        Vector2 targetPos = target.position;
        Vector2 offset = targetPos - myPos;

        float angle = Mathf.Atan2(offset.x, offset.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90f,Vector3.back);
        render.flipY = offset.x < 0 ? true : false;
    }
}
