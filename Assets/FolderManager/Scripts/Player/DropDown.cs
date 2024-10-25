using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour
{
    public string oneWayPlatformLayerName = "OneWayPlatform";
    public string playerLayerName = "Player";
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(playerLayerName), LayerMask.NameToLayer(oneWayPlatformLayerName), true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(playerLayerName), LayerMask.NameToLayer(oneWayPlatformLayerName), false);
        }
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OneWayPlatform"))
        {
            anim.SetBool(AnimationStrings.isGrounded, true);
        }
    }*/
}
