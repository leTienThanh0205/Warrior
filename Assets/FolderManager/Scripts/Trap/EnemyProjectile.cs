using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : TakeDamagePlayer
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifeTime;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate (movementSpeed,0, 0);
        lifeTime += Time.deltaTime;
        if(lifeTime > resetTime)
        {
            gameObject.SetActive (false);
        }
    }
    public void ActivateProjectile()
    {
        lifeTime = 0;
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            base.OnTriggerEnter2D(collision); //Execute logic from parent script first

        }
    }
}
