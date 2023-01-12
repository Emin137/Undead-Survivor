using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private Text text;
    private Animator animator;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        animator = GetComponentInChildren<Animator>();
    }

    public void SetDamage(float value,Vector2 pos)
    {
        text.color = Color.white;
        transform.position = pos;
        text.text = value.ToString("F0");
        Invoke("SetActive", 0.8f);
        animator.Play("Floating");
    }

    public void SetHp(float value, Vector2 pos)
    {
        text.color = Color.red;
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
