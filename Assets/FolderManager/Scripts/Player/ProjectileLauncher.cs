using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePref;
    public Transform arrowPoint;
    void Start()
    {
        
    }

    void Update()
    {
    }
    public void FireProjectile()
    {
       GameObject projectile = Instantiate(projectilePref, arrowPoint.transform.position, projectilePref.transform.rotation);
       Vector3 origScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(
            origScale.x * transform.localScale.x > 0 ? 1 : -1,
            origScale.y,
            origScale.z
            );
    }
}
