using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHealthbar : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;

    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 100 && currentHealth > 0)
        {
            anim.SetTrigger(AnimationStrings.hurtTrigger);
        }else if(currentHealth <= 0)
        {
            anim.SetTrigger(AnimationStrings.dieTrigger);
        }
    }
    private void Distances()
    {
        gameObject.SetActive(false);
    }
}
