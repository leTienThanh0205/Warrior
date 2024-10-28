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
    public GameObject effectHealth;
    public float addHealth;

    private void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)&&currentHealth < 100)
        {
            AddHealth(addHealth);
        }
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
    public void AddHealth(float health)
    {
        currentHealth += health;
        Instantiate(effectHealth, transform.position, Quaternion.identity);
        Debug.Log("AddHealh: " + currentHealth);

    }
}
