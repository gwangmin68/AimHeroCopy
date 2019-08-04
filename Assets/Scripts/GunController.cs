using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GunController : MonoBehaviour {

    //표적에 안맞을때 생성할 오브젝트
	public GameObject MissEffect;
    //게임 플레이어 카메라
	public GameObject PlayerCamera;
    Camera player_camera, weapon_camera;

    //저격소총 줌 인 효과를 위한 오브젝트들
    public GameObject GameCanvas;

    GameObject crosshair;
    GameObject zoom_crosshair;
    GameObject gamepanel;

    //무기별 연사 속도
    public float HandGunFireTime, SniperFireTime;
    //정조준 시간 - 높을수록 빨라짐
    public float FineSight_time;

    //게임 내에 적용할 연사 속도
    float FireTime;

    //발사 횟수, 명중 횟수
    private static int FireCount = 0, HitCount = 0;

    //총 오브젝트
    private GameObject gun;

    //정조준 하기 전의 총 위치, 정조준 한 후의 총 위치
    private Vector3 gunPos, FineSightPos;
    
    //정조준, 발사 상태 변수
	public bool isFineSight;
	private bool canFire;

	Animation gunAnimation;
    AudioSource gunAudio;
    
    void Awake()
    {
        //십자 크로스헤어
        crosshair = GameCanvas.transform.Find("CrossHair").gameObject;
        //줌 크로스헤어
        zoom_crosshair = GameCanvas.transform.Find("ZoomIn").gameObject;
        //게임 상단 패널
        gamepanel = GameCanvas.transform.Find("panel_gameTime").gameObject;

        //게임 모드에 따라 총 활성화
        if (GameManager.gameMode == 1)
        {
            transform.Find("Handgun").gameObject.SetActive(true);
            FireTime = HandGunFireTime;
        }
        else if (GameManager.gameMode == 2)
        {
            transform.Find("Sniper").gameObject.SetActive(true);
            FireTime = SniperFireTime;
        }

        //왜인진 모르겠지만 총이 렌더링 되지 않아서 카메라 오브젝트를 재활성화 함
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        isFineSight = false;
        canFire = true;

        //Weapon의 자식들(총) 오브젝트를 가져옴
        gun = transform.GetChild(GameManager.gameMode - 1).gameObject;

        gunPos = gun.transform.localPosition;
        FineSightPos = gunPos - new Vector3(gunPos.x, -0.06f, 0f);

        //플레이어 카메라 좌, 우 회전
        player_camera = PlayerCamera.GetComponent<Camera>();
        //무기 카메라 상, 하 회전
        weapon_camera = transform.GetComponent<Camera>();
        //총 발사 애니메이션
        gunAnimation = gun.GetComponent<Animation>();
        //총 발사 소리
        gunAudio = gun.GetComponent<AudioSource>();
        gunAudio.volume = GameObject.Find("GameManager").GetComponent<GameManager>().player_setting.sound * 0.1f;
    }

    void Update () {
		InputMouse ();
	}

    //마우스 입력
	void InputMouse()
    {
        gunAudio.volume = GameObject.Find("GameManager").GetComponent<GameManager>().player_setting.sound * 0.1f;
        if (SceneManager.GetSceneByName("Setting").isLoaded)
        {
            return;
        }
        if (Input.GetMouseButtonDown (0) && GameManager.gameStatus == true) {	//마우스 왼쪽
			if (canFire) {
                Fire();
				StartCoroutine("FireSpeed");
			}
		} else if (Input.GetMouseButtonDown (1)) {  //마우스 오른쪽, 정조준
            //정조준, 정조준 해제 시 코루틴 멈춤
            StopCoroutine("DisableFineSight");
            StopCoroutine("EnableFineSight");

            if (isFineSight) {
                //정조준 해제
                StartCoroutine("DisableFineSight");
            } else {
                //정조준
                StartCoroutine("EnableFineSight");
            }
            isFineSight = !isFineSight;
        }
	}

    private void EnableZoomIn()
    {
        //카메라 확대
        player_camera.fieldOfView = 30;

        //무기, 크로스헤어, 게임 패널 비활성화
        weapon_camera.enabled = false;
        crosshair.SetActive(false);
        gamepanel.SetActive(false);

        //줌 이미지 활성화
        zoom_crosshair.SetActive(true);
    }

    private void DisableZoomIn()
    {
        player_camera.fieldOfView = 60;

        //무기, 크로스헤어, 게임 패널 활성화
        weapon_camera.enabled = true;
        crosshair.SetActive(true);
        gamepanel.SetActive(true);

        //줌 이미지 비활성화
        zoom_crosshair.SetActive(false);
    }

    private void Fire()
    {
        FireCount++;
        gunAnimation.Play();
        gunAudio.Play();
        
        RaycastHit hit;
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit))
        {
            //표적지이면 MissEffect 생성
            if (hit.transform.tag == "ShootingSection")
            {
                Instantiate(MissEffect, hit.point, hit.transform.rotation);
            }
            //표적이면 표적 삭제
            else if (hit.transform.tag == "Overwrite")
            {
                HitCount++;
                GameManager.AddScore();
                Destroy(hit.transform.gameObject);
            }
        }
    }

    #region 코루틴
    //연사 속도 코루틴
    IEnumerator FireSpeed(){
		canFire = false;
		yield return new WaitForSeconds (FireTime);
		canFire = true;
	}

    //정조준 코루틴
    IEnumerator EnableFineSight(){
		while(gun.transform.localPosition != FineSightPos)
        {
            if (Vector3.Distance(gun.transform.localPosition, gunPos) > 0.2 && gun.transform.tag == "Sniper")
                EnableZoomIn();
            gun.transform.localPosition = Vector3.Lerp (gun.transform.localPosition, FineSightPos, FineSight_time*Time.deltaTime);
			yield return null;
        }
    }

    //정조준 해제 코루틴
	IEnumerator DisableFineSight(){
        if(gun.transform.tag == "Sniper")
            DisableZoomIn();
		while(gun.transform.localPosition != gunPos){
            gun.transform.localPosition = Vector3.Lerp (gun.transform.localPosition, gunPos, FineSight_time*Time.deltaTime);
			yield return null;
		}
	}
    #endregion

    public static int getFireCount()
    {
        return FireCount;
    }
    public static int getHitCount()
    {
        return HitCount;
    }
    public static void Refresh()
    {
        FireCount = 0;
        HitCount = 0;
    }
}
