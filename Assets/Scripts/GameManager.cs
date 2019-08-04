using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour{
    
    //게임 플레이 시간
	public float GamePlayTime;//초
    
    //게임 상태 true, false
    public static bool gameStatus = false, GameManagerExist = false;
    //게임 모드 1=권총, 2=저격소총,   맞췄을때 점수
    public static int gameMode = 1, HitScore;
    //전체 점수, 실제 흘러가는 게임 시간
    private static float Score = 0, gameTime;

    //사용자의 설정 정보를 담을 구조체
    public Player_Setting player_setting;

    private static Coroutine timeCoroutine = null;


    #region Awake, Update
    void Awake(){
        if(!GameManagerExist)
        {
            DontDestroyOnLoad(gameObject);
            GameManagerExist = true;
        }
        
        HitScore = 200;

        gameTime = GamePlayTime;

        //만들어 놓은 생성자를 쓰기 위해 값 하나 할당(아무것도 쓰지 않으면 매개 변수가 모두 선택적 매개변수지만 기본 생성자가 호출되어 값이 할당되지 않음)
        player_setting = new Player_Setting(sensivity:5);
    }
    void LateUpdate () {
        Key_Input();
	}
	#endregion

    void Key_Input()
    {
        if (Input.GetKeyDown(KeyCode.R) && !gameStatus)
        {
            
            if(GameManager.getScore() != 0)
            {
                GameManager.ResetScore();
                GunController.Refresh();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            }else if (GameManager.getScore() == 0 )
            {
                OverwriteController.CreateOverwrite();
                StartCoroutine("CalcGamePlayTime");
            }
        }
        if (gameTime <= 0)
        {
            OverwriteController.StopOverwrite();
            gameTime = GamePlayTime;
        }
    }
    
    private IEnumerator CalcGamePlayTime()
    {
        while(gameStatus)
        {
            gameTime -= Time.deltaTime;
            AddScore(0.7f * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    #region CountMethod

    public static void AddScore(){
		GameManager.AddScore(HitScore);
	}
    public static void AddScore(float addScore){
		Score += addScore;
	}
	#endregion

    public static float getScore()
    {
        return Score;
    }

    public static float getGameTime()
    {
        return gameTime;
    }
    public static void ResetScore()
    {
        Score = 0;
    }
}
