using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private TMP_Text text;
    private Animator animator;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        animator = GetComponentInChildren<Animator>();
    }

    public void InitDamage(float value,Vector2 pos)
    {
        text.color = Color.white;
        text.fontSize = 30f;
        transform.position = pos;
        text.text = value.ToString("F0");
        Invoke("SetActive", 0.8f);
        animator.Play("Floating");
    }

    public void InitHp(float value, Vector2 pos)
    {
        text.color = Color.red;
        text.fontSize = 40f;
        transform.position = pos;
        string str;
        if (value > 0)
            str = $"+{value}";
        else
            str = value.ToString();
        text.text = str;
        Invoke("SetActive", 0.8f);
        animator.Play("Floating");
    }

    private void SetActive()
    {
        gameObject.SetActive(false);
    }
}
