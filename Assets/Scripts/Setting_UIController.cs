using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_UIController : MonoBehaviour
{
    private static float sensivity_value;
    private static float zoom_sensivity_value;
    private static float sound_value;

    public void setSensivity()
    {
        sensivity_value = this.GetComponent<Slider>().value;
    }

    public void setZoomSensivity()
    {
        zoom_sensivity_value = this.GetComponent<Slider>().value;
    }

    public void setSound()
    {
        sound_value = this.GetComponent<Slider>().value;
    }

    public static float getSensivity()
    {
        return sensivity_value;
    }

    public static float getZoomSensivity()
    {
        return zoom_sensivity_value;
    }

    public static float getSound()
    {
        return sound_value;
    }
}