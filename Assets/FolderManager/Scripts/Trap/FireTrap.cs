using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damgage;
    [Header("Firetrap timer")]
    private Animator anim;
    private SpriteRenderer spr;
    public float activationDelay;
    public float activeTime;
    private bool trigged;
    private bool active;
    private HealthPlayer healthPlayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(healthPlayer != null && active)
        {
            healthPlayer.TakeDamage(damgage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!trigged)
            {
                StartCoroutine(ActivatedFireTrap());
            }
            if(active)
            {
                collision.GetComponent<HealthPlayer>().TakeDamage(damgage);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            healthPlayer = null;
        }
    }
    public IEnumerator ActivatedFireTrap()
    {
        trigged = true;
        spr.color = Color.red;
        yield return new WaitForSeconds(activationDelay);
        spr.color = Color.white;
        active = true;
        anim.SetBool(AnimationStrings.isActivated, true);
        yield return new WaitForSeconds(activeTime);
        trigged = false;
        active = false;
        anim.SetBool(AnimationStrings.isActivated, false);

    }
}
