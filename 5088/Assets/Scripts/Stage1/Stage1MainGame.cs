using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1MainGame : MonoBehaviour
{
    [SerializeField] GameObject MainGameUI;     // 메인게임 UI 저장
    [SerializeField] S1CameraAnim s1camera;     // 스테이지 1 카메라 애니메이션

    public Slider proceedSlider;                // 진행률 슬라이더
    static public float proceed = 0.0f;         // 진행률
    [SerializeField] Text proceedText;          // 진행률 텍스트
    [SerializeField] GameObject PressingAnim;   // 진행 애니메이션
    [SerializeField] AudioSource pressSound;    // 클릭 효과음

    [SerializeField] GameObject Guide1;     // 안내 텍스트1
    [SerializeField] GameObject Guide2;     // 안내 텍스트2

    bool isGuide2 = false;

    public enum MAIN_STATE
    {
        WAIT, PROCEED, LOOK, CLEAR, DEAD
    };
    static public MAIN_STATE main_state = MAIN_STATE.WAIT;

    private void OnEnable()
    {
        // 활성화될때마다
        // 진행률 초기화
        proceed = 0.0f;
        proceedSlider.value = 0;
        // 대기 상태로 전환
        main_state = MAIN_STATE.WAIT;

        // UI 활성화
        MainGameUI.SetActive(true);
        // 안내 텍스트1 활성화
        Guide1.SetActive(true);
        // 안내 텍스트2 상태 초기화
        isGuide2 = false;

        // 카메라 애니메이터 상태 초기화
        s1camera.ResetState();
    }

    void Update()
    {
        // 1. 키 입력 조건 체크
        // D키 입력시
        if (Input.GetKeyDown(KeyCode.D))
        {
            // 진행중 또는 대기중 상태일때
            if(main_state == MAIN_STATE.PROCEED || main_state == MAIN_STATE.WAIT)
            {
                // 카메라 회전
                s1camera.RotateD();
                Debug.Log("로봇을 쳐다봅니다.");

                // 로봇을 쳐다보면 마우스를 누르고 있더라도 작업은 자동으로 중지
                SetToWait();
                // 쳐다보는 상태로 전환
                main_state = MAIN_STATE.LOOK;
                // 로봇 정지 상태로 전환
                RobotManager.robot = RobotManager.ROBOT_STATE.STOP;
            }
        }
        // A키 입력시
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 로봇 쳐다보는 상태일때
            if (main_state == MAIN_STATE.LOOK)
            {
                // 카메라 회전
                s1camera.RotateA();
                Debug.Log("작업으로 돌아갑니다.");

                // 대기 상태로 전환
                main_state = MAIN_STATE.WAIT;
            }
        }

        // 2. 진행률 텍스트
        proceedText.text = proceed.ToString("F0") + "%";

        // 3. 메인 게임 상태 체크
        switch (main_state)
        {
         case MAIN_STATE.WAIT:
             // 대기 상태
             break;
         case MAIN_STATE.PROCEED:
             // 진행 중
             Proceeding();
             break;
         case MAIN_STATE.LOOK:
             // 로봇 쳐다보는 중
             break;
         case MAIN_STATE.DEAD:
             // 사망할시 게임 오버
             // 로드 시 게임 시작 처음으로 돌아감
             DeadAnim();
             break;
         case MAIN_STATE.CLEAR:
             // 스테이지1 클리어
             Stage1Clear();
             break;
        }
    }

    public void Proceeding()
    {
        // 진행률 1초마다 +1씩 올라감
        proceed += Time.deltaTime;
        proceedSlider.value = proceed;  // 진행률 슬라이더에 연결

        // 로봇은 30초 후부터 이동
        if(proceed >= 30)
        {
            // 처음이라면 가이드2 화면 뜨게
            if (!isGuide2)
            {
                Guide2.SetActive(true);
                isGuide2 = true;
            }

            // 로봇 이동 상태로 전환
            RobotManager.robot = RobotManager.ROBOT_STATE.MOVE;
        }

        // 진행률이 100% 이상이면 스테이지1 클리어!
        if (proceed >= 100)
            main_state = MAIN_STATE.CLEAR;
    }

    void DeadAnim()
    {
        Debug.Log("Game Over!");
        // 메인 게임 UI 지우기
        MainGameUI.SetActive(false);
        // 사망 애니메이션
        s1camera.AnimPlay();

        // 대기 상태로 전환
        main_state = MAIN_STATE.WAIT;
    }

    // 사망 애니메이션이 끝나면 실행됨
    public void Dead()
    {
        // 스테이지1 사망 상태로 전환
        Stage1Manager.stage1 = Stage1Manager.STAGE1.DEAD;
        // 스테이지1 메인게임 비활성화
        gameObject.SetActive(false);
    }

    public void Stage1Clear()
    {
        // 스테이지1 클리어 상태로 전환
        Stage1Manager.stage1 = Stage1Manager.STAGE1.CLEAR;

        // 메인 게임과 로봇 게임 오브젝트 비활성화
        RobotManager.robot = RobotManager.ROBOT_STATE.CLEAR;
        gameObject.SetActive(false);
    }

    // 진행 버튼 누르고 있을 때
    public void SetToProceed()
    {
        // 애니메이션 & 효과음
        PressingAnim.SetActive(true);
        pressSound.Play();
        // 진행 상태로 전환
        main_state = MAIN_STATE.PROCEED;
    }

    // 진행 버튼을 뗐을 때
    public void SetToWait()
    {
        // 애니메이션 & 효과음
        PressingAnim.SetActive(false);
        pressSound.Pause();
        // 대기 상태로 전환
        main_state = MAIN_STATE.WAIT;
    }
}
