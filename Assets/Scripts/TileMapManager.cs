using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapManager : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector2 playerPos = GameManager.instance.player.transform.position;
        Vector2 myPos = transform.position;

        float offsetX = Mathf.Abs(playerPos.x - myPos.x);
        float offsetY = Mathf.Abs(playerPos.y - myPos.y);

        Vector2 axis = GameManager.instance.player.axis;
        float axisX = axis.x < 0 ? -1 : 1;
        float axisY = axis.y < 0 ? -1 : 1;

        if(offsetX>offsetY)
        {
            transform.Translate(Vector2.right * axisX * 68);
        }
        else if(offsetY>offsetX)
        {
            transform.Translate(Vector2.up * axisY * 68);
        }
    }
}
