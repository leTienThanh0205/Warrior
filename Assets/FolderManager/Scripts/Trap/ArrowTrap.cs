using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private Transform arrowPoint;
    [SerializeField] private GameObject[] arrows;
    [SerializeField] private float attackCooldown;
     private float cooldownTimer;

    void Start()
    {
        
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    public void Attack()
    {
        cooldownTimer = 0;

            arrows[FindArrow()].transform.position = arrowPoint.position;
            arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();

    }
}
