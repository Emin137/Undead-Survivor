using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Exp,
        Heal,
        Magnet,
        Box
    }

    [System.Serializable]
    public class ItemData
    {
        public ItemType Type;
        public float value;
    }

    public ItemData itemData;
    public bool isMagnet;
    private void Update()
    {
        if(isMagnet)
        {
            Vector2 offset = GameManager.instance.player.transform.position- transform.position;
            transform.Translate(offset * 10 * Time.deltaTime);
        }    
    }

    
}
