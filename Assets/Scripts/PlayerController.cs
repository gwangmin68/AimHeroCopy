using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float player_move;

    public GameObject Cube;

    Vector3 Cube_size;
    Vector3 Player_size;

    bool ExitCollider = false;

    void Awake()
    {
        Player_size = transform.Find("Player_body").GetComponent<CapsuleCollider>().bounds.size;

        Cursor.lockState = CursorLockMode.Locked;
    }
	
	void Update () {
		InputKey ();
	}

	void InputKey()
	{
		if (GameManager.gameStatus &&  Input.GetKeyDown (KeyCode.R)) {
			//Reload
		}
		if(Input.GetKey(KeyCode.W)){
			transform.Translate (0, 0, player_move * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.A))
        {
            transform.Translate (-player_move * Time.deltaTime, 0, 0);
		}
		if(Input.GetKey(KeyCode.S))
        {
            transform.Translate (0, 0, -player_move * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.D))
        {
            transform.Translate (player_move * Time.deltaTime, 0, 0);
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
            if(!GameManager.gameStatus && GameManager.getScore() != 0)
            {
                Cursor.lockState = CursorLockMode.None;
                GameManager.ResetScore();
                GunController.Refresh();
                SceneManager.LoadScene("Main");
                return;
            }
            if(Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
                SceneManager.UnloadScene("Setting");
            }else if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("Setting", LoadSceneMode.Additive);
            }
		}
		if(Input.GetKeyDown(KeyCode.F1)){
			Cursor.lockState = CursorLockMode.None;
		}
	}
}