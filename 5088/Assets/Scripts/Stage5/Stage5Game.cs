using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage5Game : MonoBehaviour
{
    public enum S5GAME
    {
        WAIT, LOOK_R, LOOK_L, DEAD, CLEAR
    };
    static public S5GAME s5_game = S5GAME.WAIT;      // 처음에는 대기 상태

    [SerializeField] S5CameraAnim s5camera;     // 스테이지 4 카메라 애니메이션

    [SerializeField] GameObject S5GameUI;   // UI 가져오기

    static public float proceed;  // 진행률 (0 - 200)
    [SerializeField] Slider proceedSlider;  // 진행률 슬라이더 바
    [SerializeField] Text proceedText;      // 진행률 텍스트

    [SerializeField] GameObject GuideText1;

    private void OnEnable()
    {
        // 활성화시 초기화
        ResetGame();

        // 안내 텍스트1 활성화
        GuideText1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 진행률 0이하 클리핑
        if (proceed <= 0)
            proceed = 0;

        // 2. 진행률 체크
        if (proceed >= 200)
            s5_game = S5GAME.CLEAR;

        // 3. UI 값 연결
        proceedSlider.value = proceed / 2;
        proceedText.text = (proceed / 2).ToString("F0") + "%";

        // 4. 카메라 전환
        // D키 입력 시
        if (Input.GetKeyDown(KeyCode.D))
        {
            // 대기 중일 때
            if (s5_game == S5GAME.WAIT)
            {
                // 카메라 회전 (오른쪽으로)
                s5camera.RotateD();
                Debug.Log("로봇을 쳐다봅니다.");

                // 쳐다보는 상태로 전환
                s5_game = S5GAME.LOOK_R;
            }
            // 로봇 쳐다보는 중일 때
            else if(s5_game == S5GAME.LOOK_L)
            {
                // 카메라 회전 (오른쪽으로)
                s5camera.RotateD();
                Debug.Log("작업으로 돌아갑니다.");

                // 쳐다보는 상태로 전환
                s5_game = S5GAME.WAIT;
            }
        }
        // A키 입력 시
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 대기 중일 때
            if (s5_game == S5GAME.WAIT)
            {
                // 카메라 회전 (왼쪽으로)
                s5camera.RotateA();
                Debug.Log("로봇을 쳐다봅니다.");

                // 쳐다보는 상태로 전환
                s5_game = S5GAME.LOOK_L;
            }
            // 로봇 쳐다보는 중일 때
            else if (s5_game == S5GAME.LOOK_R)
            {
                // 카메라 회전 (왼쪽으로)
                s5camera.RotateA();
                Debug.Log("작업으로 돌아갑니다.");

                // 대기 상태로 전환
                s5_game = S5GAME.WAIT;
            }
        }

        // 5. 상태 체크
        switch (s5_game)
        {
            case S5GAME.WAIT:
                // 대기 상태
                break;
            case S5GAME.LOOK_R:
                // 오른쪽 로봇 쳐다보는 상태
                break;
            case S5GAME.LOOK_L:
                // 왼쪽 로봇 쳐다보는 상태
                break;
            case S5GAME.DEAD:
                // 사망
                DeadAnim();
                break;
            case S5GAME.CLEAR:
                // 스테이지5 게임 클리어
                Clear();
                break;
        }
    }

    void DeadAnim()
    {
        // UI 비활성화
        S5GameUI.SetActive(false);
        // 카메라 사망 애니메이션
        s5camera.AnimPlay();

        // 대기 상태로 전환
        s5_game = S5GAME.WAIT;
    }

    // 사망 애니메이션이 끝나면 실행됨
    public void Dead()
    {
        Debug.Log("죽었습니다!");

        // 스테이지5 매니저 사망 상태로 전환
        Stage5Manager.stage5 = Stage5Manager.STAGE5.DEAD;

        // 스테이지5 게임 비활성화
        gameObject.SetActive(false);
    }

    void Clear()
    {
        // 스테이지5 매니저 클리어 상태로 전환
        Stage5Manager.stage5 = Stage5Manager.STAGE5.CLEAR;

        // 스테이지5 게임 비활성화
        gameObject.SetActive(false);
    }

    void ResetGame()
    {
        // 초기화
        proceed = 0;
        s5_game = S5GAME.WAIT;
        // UI 활성화
        S5GameUI.SetActive(true);
        // 카메라 애니메이터 초기화
        s5camera.ResetState();
    }
}
