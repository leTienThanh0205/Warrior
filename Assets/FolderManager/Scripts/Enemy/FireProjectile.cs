using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    //[SerializeField] private Vector2 moveSpeed = new Vector2(8f, 0);
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    public int damage = 10;
    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
        //rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthPlayer health = collision.GetComponent<HealthPlayer>();
        if (collision.CompareTag("Player"))
        {
            hit = true;
           // base.OnTriggerEnter2D(collision); //Execute logic from parent script first
            coll.enabled = false;
            health.TakeDamage(damage);
            //anim.SetTrigger("explode");
            if (anim != null)
                anim.SetTrigger(AnimationStrings.fireExploded); //When the object is a fireball explode it
            else
                gameObject.SetActive(false); //When this hits any object deactivate arrow
        }
        if (collision.CompareTag("Ground"))
        {
            hit = true;
           // base.OnTriggerEnter2D(collision); //Execute logic from parent script first
            coll.enabled = false;
            if (anim != null)
                anim.SetTrigger(AnimationStrings.fireExploded); //When the object is a fireball explode it
            else
                gameObject.SetActive(false); //When this hits any object deactivate arrow
        }
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
