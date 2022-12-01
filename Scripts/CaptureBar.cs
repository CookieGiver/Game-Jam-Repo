using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureBar : MonoBehaviour
{
    public Slider slider;
    // Update is called once per frame
    public void SetValue(float num)
    {
        slider.value = num;
    }
}
