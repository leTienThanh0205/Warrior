using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSword : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            Debug.Log("attackBoss");
        }
    }
}
