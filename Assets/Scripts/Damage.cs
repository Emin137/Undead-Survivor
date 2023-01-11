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
        text = GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    public void SetDamage(float value)
    {
        text.text = value.ToString();
        Invoke("SetActive", 0.8f);
        animator.Play("Floating");
    }

    private void SetActive()
    {
        gameObject.SetActive(false);
    }
}
