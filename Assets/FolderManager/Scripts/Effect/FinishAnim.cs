using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAnim : MonoBehaviour
{
    private void FinishAnimator()
    {
        Destroy(gameObject);
    }
    private void StopObject()
    {
        gameObject.SetActive(false);
    }
}
