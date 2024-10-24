using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 moveSpeed = new Vector2(8f, 0);
    Rigidbody2D rb;
    Animator anim;
    public int damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FloatingHealthbar enemyHealthbar = collision.GetComponent<FloatingHealthbar>();
        if (collision.CompareTag("Enemy"))
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            enemyHealthbar.TakeDamage(damage);
            anim.SetTrigger(AnimationStrings.arrowExploded);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }

    public void Distances()
    {
        gameObject.SetActive(false);
    }
}

