using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Exp,
        Heal,
        Magnet
    }

    [System.Serializable]
    public class ItemData
    {
        public ItemType Type;
        public float value;
    }

    public ItemData itemData;
}
