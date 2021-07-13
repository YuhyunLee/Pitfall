using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage4Game : MonoBehaviour
{
    public enum S4GAME
    {
        WAIT, LOOK, DEAD, CLEAR
    };
    static public S4GAME s4_game = S4GAME.WAIT;      // 처음에는 대기 상태

    [SerializeField] S4CameraAnim s4camera;     // 스테이지 4 카메라 애니메이션

    [SerializeField] GameObject S4GameUI;   // UI 가져오기
    static public float proceed;            // 진행률 (0 - 200)
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
            s4_game = S4GAME.CLEAR;

        // 3. UI 값 연결
        proceedSlider.value = proceed / 2;
        proceedText.text = (proceed / 2).ToString("F0") + "%";

        // 4. 카메라 전환
        // D키 입력 시
        if (Input.GetKeyDown(KeyCode.D))
        {
            // 대기 중일 때
            if (s4_game == S4GAME.WAIT)
            {
                // 카메라 회전 (오른쪽으로)
                s4camera.RotateD();
                Debug.Log("로봇을 쳐다봅니다.");

                // 쳐다보는 상태로 전환
                s4_game = S4GAME.LOOK;
            }
        }
        // A키 입력 시
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 로봇 쳐다보는 상태일 때
            if (s4_game == S4GAME.LOOK)
            {
                // 카메라 회전 (오른쪽으로)
                s4camera.RotateA();
                Debug.Log("작업으로 돌아갑니다.");

                // 대기 상태로 전환
                s4_game = S4GAME.WAIT;
            }
        }

        // 5. 상태 체크
        switch (s4_game)
        {
            case S4GAME.WAIT:
                // 대기 상태
                break;
            case S4GAME.LOOK:
                // 로봇 쳐다보는 상태
                break;
            case S4GAME.DEAD:
                // 사망
                DeadAnim();
                break;
            case S4GAME.CLEAR:
                // 스테이지4 게임 클리어
                Clear();
                break;
        }
    }

    void DeadAnim()
    {
        // UI 비활성화
        S4GameUI.SetActive(false);
        // 카메라 사망 애니메이션
        s4camera.AnimPlay();

        // 대기 상태로 전환
        s4_game = S4GAME.WAIT;
    }

    // 사망 애니메이션이 끝나면 실행됨
    public void Dead()
    {
        Debug.Log("죽었습니다!");

        // 스테이지4 매니저 사망 상태로 전환
        Stage4Manager.stage4 = Stage4Manager.STAGE4.DEAD;

        // 스테이지4 게임 비활성화
        gameObject.SetActive(false);
    }

    void Clear()
    {
        // 스테이지4 매니저 클리어 상태로 전환
        Stage4Manager.stage4 = Stage4Manager.STAGE4.CLEAR;

        // 스테이지4 게임 비활성화
        gameObject.SetActive(false);
    }

    void ResetGame()
    {
        // 초기화
        proceed = 0;
        s4_game = S4GAME.WAIT;
        // UI 활성화
        S4GameUI.SetActive(true);
        // 카메라 애니메이터 초기화
        s4camera.ResetState();
    }
}
