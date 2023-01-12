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
    public Text level;
    public Image imageDead;

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

    public void SetHP(float value)
    {
        hpSlider.value = value;
    }

    public void SetExp(float value)
    {
        expSlider.value = value;
    }

    public void SetLevel(float exp,float maxExp,int level)
    {
        this.level.text = $"Lv.{level}";
        expSlider.value = exp;
        expSlider.maxValue = maxExp;
    }

    public void SetKillText(int value)
    {
        string text = string.Format("{0:000}", value);
        killCount.text = text;
    }
}
