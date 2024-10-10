using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamagePlayer : MonoBehaviour
{
    public float damage = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthPlayer health = collision.GetComponent<HealthPlayer>();

        if (collision.CompareTag("Player"))
        {
            health.TakeDamage(damage);
        }
    }
}
