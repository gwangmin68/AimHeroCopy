using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    Slider sensivity, zoom_senseivity, sound;

    private void Awake()
    {
        sensivity = GameObject.Find("slider_sensivity").GetComponent<Slider>();
        zoom_senseivity = GameObject.Find("slider_zoom_sensivity").GetComponent<Slider>();
        sound = GameObject.Find("slider_sound").GetComponent<Slider>();
        
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sensivity.value = manager.player_setting.sensivity;
        zoom_senseivity.value = manager.player_setting.zoom_sensivity;
        sound.value = manager.player_setting.sound;
    }
}
