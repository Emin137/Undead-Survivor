using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text time;
    public Text hpText;
    public Slider hpSlider;
    public Slider expSlider;
    public Slider attackSpeedSlider;
    public Text killCount;
    public Text level;
    public Text stage;
    public Image imageDead;
    public Toggle[] toggles;
    public GameObject popupLevelup;
    public GameObject popupGameOver;
    public Button buttonGame;

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
        hpText.text = $"{value}/{hpSlider.maxValue}";
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

    public void OnValueChanaged(int toggleIndex)
    {
        Color color = new Color(1, 1, 1, 0.2f);
        toggles[toggleIndex].image.color = toggles[toggleIndex].isOn ? Color.white : color;
        GameManager.instance.player.weaponIndex =  toggleIndex;
    }

    public void OnLevelUp()
    {
        popupLevelup.SetActive(true);
    }

    public void OnGameOver()
    {
        popupGameOver.SetActive(true);
    }

    public void BtnGame()
    {
        GameManager.instance.player.TimeStop(1);
        popupLevelup.SetActive(false);
    }

    public void BtnTitle()
    {
        GameManager.instance.player.TimeStop(1);
        SceneManager.LoadScene(0);
    }

}
