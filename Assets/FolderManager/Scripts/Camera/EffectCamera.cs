using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
public class EffectCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public ShakeData explosionShakeData;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.I))
        {
            CameraShakerHandler.Shake(explosionShakeData);
        }
    }
}
