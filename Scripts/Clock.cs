using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Clock : MonoBehaviour
{
    public Light2D worldLight;
    public Light2D playerLight;
    public double time;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, (float)Time.timeAsDouble * 360/1440);
        time = Time.timeAsDouble / 40;

        if (0 <= time && time <= 18)
        {
            worldLight.intensity = (float)((9 - Math.Abs(time%24 - 9))/18 + 0.5);
        }
        else
        {
            worldLight.intensity = 0.3f;
        }
        playerLight.intensity = 1 - worldLight.intensity;
        playerLight.pointLightOuterRadius = 3.55f + (float)Math.Sin(2 * Time.timeAsDouble)/2;
    }
}
