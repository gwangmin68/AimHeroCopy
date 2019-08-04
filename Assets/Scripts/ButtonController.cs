using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

    ToggleGroup group;
    
    //메인 씬 게임 시작 버튼
    public void onClickStart()
    {
        //메인 화면 게임 모드 토글 버튼 그룹
        group = GameObject.Find("ToggleGroup").transform.GetComponent<ToggleGroup>();

        //체크된 버튼 가져와서 게임 모드 설정
        foreach (var item in group.ActiveToggles())
        {
            switch (item.name)
            {
                case "Mode_Pistol":
                    GameManager.gameMode = 1;
                    break;
                case "Mode_Sniper":
                    GameManager.gameMode = 2;
                    break;
            }
        }

        GameManager.ResetScore();
        GunController.Refresh();
        //게임 씬 로드
        SceneManager.LoadScene("Game");
    }

    //메인 씬 설정 버튼
    public void onClickSetting()
    {
        //설정 씬 로드
        SceneManager.LoadScene("Setting", LoadSceneMode.Additive);
    }

    //설정 씬 저장 버튼
    public void onclickSettingQuit()
    {
        //설정 씬 언로드
        SceneManager.UnloadScene("Setting");

        //게임 중이면 커서 고정&숨기기
        if(SceneManager.GetActiveScene().name.Equals("Game"))
            Cursor.lockState = CursorLockMode.Locked;
    }

    //메인 씬 게임 종료 버튼
    public void onClickQuit()
    {
        //게임 완전 종료
        Application.Quit();
    }

    public void saveSetting()
    {
        GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gamemanager.player_setting.sensivity = Setting_UIController.getSensivity();
        gamemanager.player_setting.zoom_sensivity = Setting_UIController.getZoomSensivity();
        gamemanager.player_setting.sound = Setting_UIController.getSound();
        onclickSettingQuit();
    }
}
