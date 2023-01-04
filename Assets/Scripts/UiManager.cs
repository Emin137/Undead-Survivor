using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text time;
    public Slider hpSlider;

    private void Start()
    {
        hpSlider.maxValue = GameManager.instance.player.playerData.hp;
        hpSlider.value = hpSlider.maxValue;
    }

    private void FixedUpdate()
    {
        HpMove();
    }

    private void HpMove()
    {
        hpSlider.transform.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position) + new Vector3(0,25f,0);
    }

    public void SetTime(int value)
    {
        string timer = string.Format("{0:00}:{1:00}", value/60,value%60);
        time.text = timer;
    }

    public void SetHp(float value)
    {
        hpSlider.value = value;
    }
}
