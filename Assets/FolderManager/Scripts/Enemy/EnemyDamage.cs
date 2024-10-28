using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
         HealthPlayer health = collision.GetComponent<HealthPlayer>();

        if (collision.CompareTag("Player"))
        {
            health.TakeDamage(10f);
        }
    }
}
