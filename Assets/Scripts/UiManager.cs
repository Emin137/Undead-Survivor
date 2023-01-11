using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text time;
    public Slider hpSlider;
    public Slider expSlider;
    public Text killCount;

    private void Start()
    {
        hpSlider.maxValue = GameManager.instance.player.playerData.hp;
        hpSlider.value = hpSlider.maxValue;
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

    public void SetExp(float value)
    {
        expSlider.value = value;
    }

    public void SetKillText(int value)
    {
        string text = string.Format("{0:000}", value);
        killCount.text = text;
    }
}
