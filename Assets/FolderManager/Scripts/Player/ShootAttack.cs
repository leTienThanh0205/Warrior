using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootAttack : MonoBehaviour
{
    Animator anim;
    TouchingDirection touching;
    public GameObject projectilePrefab;
    public Transform firePoint;
    void Start()
    {
        anim = GetComponent<Animator>();
        touching = GetComponent<TouchingDirection>();
       // audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShootAttack(InputAction.CallbackContext context)
    {
        if(context.started && touching.IsGrounded)
        {
            anim.SetTrigger(AnimationStrings.shootAttackTrigger);
        }
    }
    void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
