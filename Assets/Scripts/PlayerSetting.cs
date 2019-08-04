using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Player_Setting
{
    public float sensivity;
    public float zoom_sensivity;
    public float sound;

    public Player_Setting(float sensivity = 5f, float zoom_sensivity = 5f, float sound = 5f)
    {
        this.sensivity = sensivity;
        this.zoom_sensivity = zoom_sensivity;
        this.sound = sound;
    }
}
