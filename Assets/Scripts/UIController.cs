using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    //게임 화면 상단 게임 시간 패널
    public GameObject panel_gameTime;

    static Text gameTime;
    static Text gameScore;
    static Text CountHit;

    void Awake()
    {
        gameTime = panel_gameTime.transform.Find("text_gameTime").GetComponent<Text>();
        gameScore = panel_gameTime.transform.Find("text_score").GetComponent<Text>();
        CountHit = panel_gameTime.transform.Find("text_hit").GetComponent<Text>();

        gameTime.text = "" + GameManager.getGameTime();
        gameScore.text = "0";
        CountHit.text = "0";
    }

    void Update()
    {
        RefreshUI();
        setResultCanvas();
    }

    private void setResultCanvas()
    {
        if (GameManager.gameStatus || GameManager.getScore() == 0)
            return;

        GameObject resultCanvas = gameObject.transform.Find("panel_gameResult").gameObject;
        resultCanvas.SetActive(true);
        resultCanvas.transform.Find("text_gamemode").GetComponent<Text>().text = "게임 모드 : " + (GameManager.gameMode == 1 ? "Pistol" : "Sniper");
        resultCanvas.transform.Find("text_accuracy").GetComponent<Text>().text = "명중률 : " + (GunController.getFireCount() == 0 ? 0 : (Mathf.Round((float)(GunController.getHitCount() * 1.0 / GunController.getFireCount()) * 100))) + " %";
        resultCanvas.transform.Find("text_fire_count").GetComponent<Text>().text = "발사 수 : " + GunController.getFireCount();
        resultCanvas.transform.Find("text_hit_count").GetComponent<Text>().text = "명중 수 : " + GunController.getHitCount();
        resultCanvas.transform.Find("text_score").GetComponent<Text>().text = "점수 : " + Mathf.Round(GameManager.getScore());
    }

    //게임 화면 UI 새로고침
    public static void RefreshUI()
    {
        gameTime.text = "" + Mathf.Round(GameManager.getGameTime());
        gameScore.text = "" + Mathf.Round(GameManager.getScore());
        CountHit.text = "" + GunController.getHitCount();
    }
}
