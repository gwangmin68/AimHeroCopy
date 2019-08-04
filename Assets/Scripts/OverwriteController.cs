using System.Collections;
using UnityEngine;

public class OverwriteController : MonoBehaviour
{
    //표적
    public GameObject overwrite;
    //표적 생성 위치
    private Vector3 overwrite_Spawn;

    Vector3 SectionPosition;

    static OverwriteController instance = null;

    static Coroutine Coroutine_Instance;

    //표적지 넓이, 높이, 두께
    float width;
    float height;
    float thick;

    void Awake()
    {
        if (instance == null)
            instance = this;

        //표적지 너비, 높이, 두께 구하기
        SectionPosition = transform.position;
        Vector3 SectionSize = transform.GetComponent<BoxCollider>().bounds.size;

        width = SectionSize.x;
        height = SectionSize.y;
        thick = SectionSize.z;
    }

    public static void CreateOverwrite()
    {
        if (!GameManager.gameStatus)
        {
            GameManager.gameStatus = true;
            Coroutine_Instance = instance.StartCoroutine("Coroutine_CreateOverwrite");
        }
    }

    public static void StopOverwrite()
    {
        GameManager.gameStatus = false;
        instance.StopCoroutine(Coroutine_Instance);
    }

    #region Coroutine
    private IEnumerator Coroutine_CreateOverwrite(){
        while (true)
        {
            float xPosition = Random.Range(
                SectionPosition.x - (width / 2),
                SectionPosition.x + (width / 2)
            );

            float yPosition = Random.Range(
                SectionPosition.y - (height / 2),
                SectionPosition.y + (height / 2)
            );

            //표적지의 Z위치 - 표적지 두께 절반
            float zPosition = SectionPosition.z - (thick / 2);

            //표적지 안의 랜덤 위치 설정
            overwrite_Spawn = new Vector3(xPosition, yPosition, zPosition);

            //표적 생성
            Instantiate(overwrite, overwrite_Spawn, overwrite.transform.rotation);

            //0.5초 기다리기
            yield return new WaitForSeconds(0.5f);
        }
    }
    #endregion
}