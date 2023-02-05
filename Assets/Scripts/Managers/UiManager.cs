using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

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
    public GameObject popupGameClear;
    public Button buttonGame;
    public Text[] rifleTexts;
    public Text[] handgunTexts;
    public Text[] shotgunTexts;
    public Text[] playerTexts;
    

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
        hpSlider.maxValue = GameManager.instance.player.playerData.maxHp;
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
        InitLevelupText(rifleTexts,0);
        InitLevelupText(handgunTexts,1);
        InitLevelupText(shotgunTexts,2);
        InitPlayerTexts();
    }

    public void OnGameOver()
    {
        popupGameOver.SetActive(true);
    }

    public void OnGameClear()
    {
        popupGameClear.SetActive(true);
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

    public void BtnRandom()
    {
        int rand = Random.Range(0, 3);
        Player.WeaponData weaponData;

        switch (rand)
        {
            case 0:
                weaponData = GameManager.instance.player.weaponDatas[0];
                weaponData.weaponDamage *= 1.5f;
                weaponData.level++;
                if (weaponData.level % 2 == 0)
                    weaponData.weaponSpeed *=0.8f;
                if (weaponData.level % 3 == 0)
                    weaponData.weaponSpecial ++;
                InitLevelupText(rifleTexts, 0);
                break;
            case 1:
                weaponData = GameManager.instance.player.weaponDatas[1];
                weaponData.weaponDamage *= 1.1f;
                weaponData.level++;
                if (weaponData.level % 2 == 0)
                    weaponData.weaponSpeed *=0.9f;
                if (weaponData.level % 3 == 0)
                    weaponData.weaponSpecial += 5;
                InitLevelupText(handgunTexts, 1);
                break;
            case 2:
                weaponData = GameManager.instance.player.weaponDatas[2];
                weaponData.weaponDamage *= 1.5f;
                weaponData.level++;
                if (weaponData.level % 2 == 0)
                    weaponData.weaponSpeed *=0.75f;
                if (weaponData.level % 3 == 0)
                    weaponData.weaponSpecial +=2;
                InitLevelupText(shotgunTexts, 2);
                break;
        }
    }

    public void InitLevelupText(Text[] texts,int index)
    {
        Player.WeaponData weaponData = GameManager.instance.player.weaponDatas[index];
        texts[0].text = $"{weaponData.weaponType.ToString()} +{weaponData.level}";
        texts[1].text = $"���ݷ� :{string.Format("{0:0.#}", weaponData.weaponDamage)}";
        texts[2].text = $"���ݼӵ� : {string.Format("{0:0.#}",1/weaponData.weaponSpeed)}";
        texts[3].text = $"���ݻ�Ÿ� : {weaponData.weaponRange}";
        switch (index)
        {
            case 0:
                texts[4].text = $"���� �� : {weaponData.weaponSpecial}";
                break;
            case 1:
                texts[4].text = $"<size=3>�߰� ġ��ŸȮ��</size> : {weaponData.weaponSpecial}%";
                break;
            case 2:
                texts[4].text = $"��ź �� : {weaponData.weaponSpecial}";
                break;
        }
    }

    public void InitPlayerTexts()
    {
        Player.PlayerData playerData = GameManager.instance.player.playerData;
        playerTexts[0].text = $"Lv.{playerData.level} �÷��̾�";
        playerTexts[1].text = $"�ִ�ü�� : {playerData.maxHp}";
        playerTexts[2].text = $"�̵��ӵ� : {playerData.speed}";
        playerTexts[3].text = $"ġ��Ÿ Ȯ�� : {playerData.crit}";
        playerTexts[4].text = $"ġ��Ÿ ������ : {playerData.critdmg}";
    }
}
