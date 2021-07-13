using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotManager : MonoBehaviour
{
    float robotCount = 0.0f;    // 로봇 카운트
    [SerializeField]
    static public int robotLevel = 0;   // 로봇 레벨
    [SerializeField]
    GameObject[] robots = new GameObject[4];    // 로봇 오브젝트 배열로 저장
    [SerializeField]
    GameObject[] robotImg;   // 로봇 이미지 배열
    [SerializeField]
    Slider robotSlider; // 로봇 슬라이더

    Stage1MainGame s1main;     // 로봇이 속한 게임매니저 가져오기

    public enum ROBOT_STATE
    {
        WAIT, STOP, MOVE, RESET, CLEAR
    };
    static public ROBOT_STATE robot = ROBOT_STATE.WAIT;

    private void Start()
    {
        // 스테이지1 메인 게임 매니저 할당
        s1main = GameObject.Find("S1MainGameManager").GetComponent<Stage1MainGame>();
    }

    private void OnEnable()
    {
        // 활성화될때마다 로봇 정보 초기화
        robot = ROBOT_STATE.RESET;
    }

    void Update()
    {
        // 로봇 슬라이더 값 연결
        robotSlider.value = robotCount;

        // 상태 확인
        switch (robot)
        {
            case ROBOT_STATE.WAIT:
                // 대기 상태
                break;
            case ROBOT_STATE.STOP:
                // 정지
                StopRobot();
                break;
            case ROBOT_STATE.MOVE:
                // 움직임
                MoveRobot();
                break;
            case ROBOT_STATE.RESET:
                // 로봇 초기화
                ResetRobot();
                break;
            case ROBOT_STATE.CLEAR:
                ClearRobot();
                break;
        }
    }

    void StopRobot()
    {
        // 로봇 카운트 초기화
        robotCount = 0.0f;
        Debug.Log("로봇 정지되었습니다. 로봇카운트 " + robotCount +"로 초기화되었습니다.");

        // 대기 상태로 전환
        robot = ROBOT_STATE.WAIT;
    }

    void MoveRobot()
    {
        robotCount += Time.deltaTime;

        if (robotCount >= 10)
        {
            robotImg[robotLevel].SetActive(true);
            robotLevel += 1;    // 로봇 +1단계

            for (int i = 0; i < 4; i++)
                robots[i].SendMessage("Move");   // 로봇 한 단계 이동

            // 4단계인데 진행률이 100퍼가 아니라면
            if (robotLevel >= 4 && Stage1MainGame.proceed != 100)
            {
                // 4단계면 플레이어 사망
                Stage1MainGame.main_state = Stage1MainGame.MAIN_STATE.DEAD;
                // 대기 상태로 전환
                robot = ROBOT_STATE.WAIT;
                return; // 함수 종료
            }

            robotCount = 0.0f;
        }
    }

    void ResetRobot()
    {
        // 로봇 정보 초기화
        robotCount = 0.0f;
        robotLevel = 0;

        // 로봇 위치 초기화
        for (int i = 0; i < 4; i++)
            robots[i].SendMessage("Reset");

        // 로봇 이미지 초기화
        for (int i = 0; i < 4; i++)
            robotImg[i].SetActive(false);

        // 대기 상태로 전환
        robot = ROBOT_STATE.WAIT;
    }

    void ClearRobot()
    {
        // 로봇 비활성화
        this.gameObject.SetActive(false);
    }

    public void AnimEnd()
    {
        // 메인게임의 Dead 함수 실행
        s1main.Dead();
    }
}

