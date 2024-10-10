using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootTeleport : MonoBehaviour
{
    public GameObject projectilePrefab;  // Prefab của projectile
    public Transform firePoint;          // Điểm nơi projectile được bắn ra
    public Vector2 projectileSpeed = new Vector2(10f, 0);// Tốc độ của projectile
    GameObject currentProjectile;
    //effect teleport
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public GameObject echo;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void ShootProjectile()
    {
        // Tạo ra projectile tại firePoint và bắn ra theo hướng nhân vật đang đối mặt

        currentProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = currentProjectile.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(projectileSpeed.x * transform.localScale.x, projectileSpeed.y);
    }
    private void Update()
    {
     
    }
    public void Dash()
    {
      
    }
    public void TeleportToProjectile()
    {
        if (currentProjectile != null)
        {
            // Dịch chuyển nhân vật tới vị trí của projectile
            GameObject currentEffect = Instantiate(echo, transform.position, Quaternion.identity);
            Vector3 origScale = currentEffect.transform.localScale;
            currentEffect.transform.localScale = new Vector3(
                origScale.x * transform.localScale.x > 0 ? 1 : -1,
                origScale.y,
                origScale.z
                );
            Destroy(currentEffect, 0.75f);
            transform.position = currentProjectile.transform.position;
            Destroy(currentProjectile);  // Xóa projectile sau khi dịch chuyển
        }
    }
    public void OnShootTeleport(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetTrigger(AnimationStrings.teleportTrigger);
        }
    }
    public void OnTeleport(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            TeleportToProjectile();//dịch chuyển
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Dash();//dịch chuyển
        }
    }
}
