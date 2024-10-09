using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public GameObject projectilePrefab;
    public float lifeTime = 4f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            /*Instantiate(projectilePrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);*/
             rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            Destroy(gameObject, lifeTime);
        }
    }
}
