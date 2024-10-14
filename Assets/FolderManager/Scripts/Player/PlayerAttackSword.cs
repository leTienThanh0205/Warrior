using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSword : MonoBehaviour
{
    public int damage = 10;
    public int damage2 = 20;
    public Transform point;
    public GameObject effect;
    void Start()
    {

    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FloatingHealthbar floatingHealthbar = collision.GetComponent<FloatingHealthbar>();
       // EffectSwordAttack();
        if (collision.CompareTag("Boss"))
        {
            floatingHealthbar.TakeDamage(damage);
        }
        if (collision.CompareTag("GruzMother"))
        {
            floatingHealthbar.TakeDamage(damage);
        }
        if (collision.CompareTag("Enemy"))
        {
            floatingHealthbar.TakeDamage(damage);
        }
    }
    /*private void EffectSwordAttack()
    {
        Instantiate(effect, point.position, Quaternion.identity);

    }*/
}
