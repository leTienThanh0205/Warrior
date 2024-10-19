using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private Transform destination;
    GameObject player;
    Rigidbody2D playerRb;
    Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            {
                StartCoroutine(PortalIn());
            }
        }
    }
    private IEnumerator PortalIn()
    {
        playerRb.simulated = false;
        anim.SetBool(AnimationStrings.isMoving, false);
        StartCoroutine(MoveInPortal());
        yield return new WaitForSeconds(0.5f);
        player.transform.position = destination.position;
        yield return new WaitForSeconds(0.5f);
        playerRb.simulated = true;
    }
    IEnumerator MoveInPortal()
    {
        float timer = 0;
        if(timer < 0.5f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position,transform.position,3*Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}
