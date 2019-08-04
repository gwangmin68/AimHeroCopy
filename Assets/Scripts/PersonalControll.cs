using UnityEngine;
using UnityEngine.SceneManagement;

public class PersonalControll : MonoBehaviour {

    //상 하 화면 회전 제한
	public float UpDownRange;
    //화면 회전 감도
    
    //좌우 입력 값
	private float LeftRight = 0f;
    //상하 입력 값
	private float UpDown = 0f;

    GameManager gamemanager;
    GunController gun;

    private void Awake()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //총 종류에 따라 정조준을 달리하기 위해 가져오는 컴포넌트
        gun = GameObject.Find("Weapon").GetComponent<GunController>();
    }

    void Update () {
		ScreenRotate ();
	}

    //마우스 입력 값에 따라 화면 회전 (1인칭 회전)
    //2개의 카메라가 있음
	void ScreenRotate(){
        if (SceneManager.GetSceneByName("Setting").isLoaded)
        {
            return;
        }
        float input_lr = Input.GetAxis ("Mouse X");
        if (gun.isFineSight && gun.gameObject.transform.GetChild(GameManager.gameMode - 1).name.Equals("Sniper"))
            LeftRight += (input_lr * gamemanager.player_setting.zoom_sensivity);//마우스 좌, 우 입력을 더함
        else
            LeftRight += (input_lr * gamemanager.player_setting.sensivity);//마우스 좌, 우 입력을 더함

        this.transform.rotation = Quaternion.Euler(0f, LeftRight, 0f);//Player의 rotation을 설정


        float input_ud = Input.GetAxis("Mouse Y");

        if (gun.isFineSight && gun.gameObject.transform.GetChild(GameManager.gameMode - 1).name.Equals("Sniper"))
            UpDown -= (input_ud * gamemanager.player_setting.zoom_sensivity);//마우스 상, 하 입력을 뺌(마우스를 위로 올리면 음수가 나옴)
        else
            UpDown -= (input_ud * gamemanager.player_setting.sensivity);//마우스 상, 하 입력을 뺌(마우스를 위로 올리면 음수가 나옴)
        UpDown = Mathf.Clamp (UpDown, -UpDownRange, UpDownRange);//상, 하 최대 회전을 90도로 고정
		this.transform.GetChild(1).transform.localRotation = Quaternion.Euler(UpDown, 0f, 0f);	//카메라의 rotation을 Player 기준 UpDown으 설정
	}
}

