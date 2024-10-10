using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthPlayer : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    Animator anim;
    Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 100 && currentHealth > 0)
        {
            anim.SetTrigger(AnimationStrings.hurtTrigger);
           // anim.SetBool(AnimationStrings.isAlive, true);
        }
        else if (currentHealth <= 0)
        {
             anim.SetTrigger(AnimationStrings.dieTrigger);
            rb.transform.localScale = Vector3.zero;

        }
        Debug.Log("Healh: " + currentHealth);
    }
}
