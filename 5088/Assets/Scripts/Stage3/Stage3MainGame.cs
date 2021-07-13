using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3MainGame : MonoBehaviour
{
    [SerializeField] GameObject KeyCardUI;   // UI 저장

    float checkTime = 0.0f; // 키 카드 검사 시간
    int checkNum = 0;       // 키 카드 검사 횟수

    float alarmTime = 15.0f;    // 경보 제한 시간 (= 이미지 게임 제한 시간)
    [SerializeField] GameObject alarmText;        // 경보 제한 시간 텍스트

    [SerializeField] GameObject GuideText1;    // 이미지 게임 안내
    [SerializeField] GameObject RedLight;      // 조명

    S3CameraAnim s3camera;  // 카메라 애니메이션
    Robot3 robot3;          // 로봇 애니메이션

    public bool s3_3 = false;
    public bool s3_4 = false;
    public bool s3_5 = false;
    public bool s3_6 = false;
    public bool s3_7 = false;
    public bool s3_8 = false;
    public bool s3_9 = false;

    public int ccount = 0;

    public enum S3MAINGAME
    {
        WAIT, CHECK, ALARM, ALARMOFF, SUCCESS, FAIL, DEAD, CLEAR
    };
    static public S3MAINGAME s3_main = S3MAINGAME.WAIT;      // 처음에는 대기 상태

    private void Awake()
    {
        s3camera = GameObject.Find("Stage3_Camera").GetComponent<S3CameraAnim>();
        robot3 = GameObject.Find("AlienFighter (1)").GetComponent<Robot3>();
    }

    private void OnEnable()
    {
        // 초기화
        ResetGame();

        // UI 활성화
        KeyCardUI.SetActive(true);
        // 카메라 위치 초기화
        s3camera.ResetState();
    }

    void Update()
    {
        // 상태 체크
        switch (s3_main)
        {
            case S3MAINGAME.WAIT:
                // 대기 상태
                break;
            case S3MAINGAME.CHECK:
                // 키 카드 검사
                CheckKeyCard();
                break;
            case S3MAINGAME.ALARM:
                // 경보 발생
                Alarm();
                break;
            case S3MAINGAME.ALARMOFF:
                // 경보 꺼주기
                AlarmOff();
                break;
            case S3MAINGAME.SUCCESS:
                // 키 카드 검사 성공
                Success();
                break;
            case S3MAINGAME.FAIL:
                // 키 카드 검사 실패
                Fail();
                break;
            case S3MAINGAME.DEAD:
                // 사망
                DeadAnim();
                break;
            case S3MAINGAME.CLEAR:
                // 클리어
                Stage3Clear();
                break;
        }
            
    }

    void CheckKeyCard()
    {
        // 검사 시간 세기 시작
        checkTime += Time.deltaTime;

        // 검사 UI 활성화
        KeyCardUI.transform.Find("KeyCardText").gameObject.SetActive(false);
        KeyCardUI.transform.Find("CheckingAnim").gameObject.SetActive(true);
        KeyCardUI.transform.Find("CheckingAnim2").gameObject.SetActive(true);
        // 진행 효과음


        // 8초가 지났다면
        if (checkTime >= 8)
        {
            // 검사 시간 초기화
            checkTime = 0.0f;
            // 검사 UI 비활성화
            KeyCardUI.transform.Find("KeyCardText").gameObject.SetActive(true);
            KeyCardUI.transform.Find("CheckingAnim").gameObject.SetActive(false);
            KeyCardUI.transform.Find("CheckingAnim2").gameObject.SetActive(false);
            // 효과음 정지


            // 검사 횟수가 4회 미만이면
            if (checkNum < 5)
            {
                // 체크 횟수 +1
                checkNum += 1;
                s3_main = S3MAINGAME.FAIL;      // 검사 실패
            }
            // 4회(이상)이면
            else
                s3_main = S3MAINGAME.SUCCESS;   // 검사 성공
        }
    }

    void Alarm()
    {
        if (checkNum == 1)
        {
            if (ccount == 0)
            {
                s3_3 = true;
                ccount = 1;
            }
        }
            
        

        // 경보 시작
        RedLight.GetComponent<Animation>().enabled = true;
        RedLight.GetComponent<AudioSource>().enabled = true;

        // 경보 시간 카운트다운 시작
        alarmTime -= Time.deltaTime;
        // 경보 텍스트 활성화
        alarmText.SetActive(true);
        alarmText.GetComponent<Text>().text = alarmTime.ToString("F0");

        // 15초 카운트 다운이 끝나면
        if(alarmTime <= 0)
        {
            s3_main = S3MAINGAME.DEAD;  // 사망
        }
    }

    void AlarmOff()
    {
        if (checkNum == 1)
            s3_4 = true;
        else if (checkNum == 2)
            s3_5 = true;
        else if (checkNum == 3)
            s3_6 = true;
        else if (checkNum == 4)
            s3_7 = true;
        else if (checkNum == 5)
            s3_8 = true;
        

        Debug.Log("경보 해제");

        // 경보 끄고
        RedLight.GetComponent<Animation>().enabled = false;
        RedLight.GetComponent<Light>().intensity = 1.0f;
        RedLight.GetComponent<AudioSource>().enabled = false;

        // 창문 뒤 로봇 애니메이션 재생
        robot3.AlarmOff();

        // 경보 제한 시간 초기화
        alarmTime = 15.0f;
        // 텍스트 비활성화
        alarmText.SetActive(false);

        // 대기 상태로 전환
        s3_main = S3MAINGAME.WAIT;
    }

    void Success()
    {
        Debug.Log("키 카드 검사 성공!");

        // 클리어 상태로 전환
        s3_main = S3MAINGAME.CLEAR;
    }

    void Fail()
    {
        Debug.Log("키 카드 검사 실패.");

        // 키 카드 활성화 (다음 검사 때 사용해야하기 때문에)
        KeyCardUI.transform.Find("KeyCard").gameObject.SetActive(true);

        // 이미지 게임 시작
        StartImgGame();

        // 경보 상태로 전환
        s3_main = S3MAINGAME.ALARM;
    }

    void DeadAnim()
    {
        Debug.Log("죽었습니다!");

        // 이미지 게임 비활성화
        gameObject.transform.Find("ImgGameManager3").gameObject.SetActive(false);
        // 키 카드 UI 비활성화
        KeyCardUI.SetActive(false);
        // 경보 해제
        RedLight.GetComponent<Animation>().enabled = false;
        RedLight.GetComponent<Light>().intensity = 1.0f;
        RedLight.GetComponent<AudioSource>().enabled = false;

        // 카메라 사망 애니메이션 실행
        s3camera.AnimPlay();
        // 대기 상태로 전환
        s3_main = S3MAINGAME.WAIT;
    }

    // 사망 애니메이션이 끝나면 실행
    public void Dead()
    {
        // 스테이지3 매니저 사망 상태로 전환
        Stage3Manager.stage3 = Stage3Manager.STAGE3.DEAD;

        // 스테이지3 게임 비활성화
        gameObject.SetActive(false);
    }

    void Stage3Clear()
    {
        s3_9 = true;
        // 경보 해제
        RedLight.GetComponent<Animation>().enabled = false;
        RedLight.GetComponent<Light>().intensity = 1.0f;
        RedLight.GetComponent<AudioSource>().enabled = false;

        // 스테이지3 매니저 클리어 상태로 전환
        Stage3Manager.stage3 = Stage3Manager.STAGE3.CLEAR;

        // 스테이지3 게임 비활성화
        gameObject.SetActive(false);
    }

    void ResetGame()
    {
        // 초기화
        checkTime = 0.0f;
        checkNum = 0;
        alarmTime = 15.0f;

        // 텍스트 비활성화
        alarmText.SetActive(false);
    }

    void StartImgGame()
    {
        // 첫 번째 이미지 게임일 경우 안내 텍스트
        if(checkNum <= 1)
        {
            // 안내 텍스트 활성화
            GuideText1.SetActive(true);
            // 이미지 게임 활성화
            gameObject.transform.Find("ImgGameManager3").gameObject.SetActive(true);
        }
        else
        {
            // 이미지 게임 활성화
            gameObject.transform.Find("ImgGameManager3").gameObject.SetActive(true);
        }
    }
}
