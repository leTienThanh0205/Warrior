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
        //  Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        GameObject projectile = Instantiate(projectilePrefab, firePoint.transform.position, projectilePrefab.transform.rotation);
        Vector3 origScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(
            origScale.x * transform.localScale.x > 0 ? 1 : -1,
            origScale.y,
            origScale.z
            );
    }

}
