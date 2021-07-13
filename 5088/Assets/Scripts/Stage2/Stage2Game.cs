using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Game : MonoBehaviour
{
    GameObject Stage2GameUI;        // 스테이지2 UI 저장
    GameObject LRButtonUI;          // 왼쪽,오른쪽 버튼 UI 저장
    SoundButton SBManagerL;    // 사운드 버튼 매니저
    SoundButton SBManagerR;    // 사운드 버튼 매니저

    [SerializeField] S2CameraAnim s2camera;     // 스테이지 2 카메라 애니메이션

    static public int countCard;    // 클릭한 카드 개수
    static public int countAnswer;  // 정답인 카드 개수

    [SerializeField] GameObject GuideText1;     // 안내 텍스트1
    [SerializeField] GameObject GuideText2;     // 안내 텍스트2

    bool isGuide2 = false;

    public enum ST2GAME
    {
        START, WAIT, CHECK, LOOK, CLEAR, DEAD
    };
    static public ST2GAME st2_game = ST2GAME.WAIT;  // 처음에는 대기 상태

    private void Awake()
    {
        Stage2GameUI = gameObject.transform.Find("Stage2GameUI").gameObject;
        LRButtonUI = gameObject.transform.Find("LRButtonUI").gameObject;
        SBManagerL = gameObject.transform.Find("SBManagerL").gameObject.GetComponent<SoundButton>();
        SBManagerR = gameObject.transform.Find("SBManagerR").gameObject.GetComponent<SoundButton>();
    }

    private void OnEnable()
    {
        // UI 활성화
        Stage2GameUI.SetActive(true);
        // 안내 텍스트1 활성화
        GuideText1.SetActive(true);
        // 안내 텍스트2 상태 초기화
        isGuide2 = false;

        // 카메라 애니메이터 상태 초기화
        s2camera.ResetState();
        // 바로 시작
        st2_game = ST2GAME.START;
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 키 입력시 조건 체크
        // W키 입력 시
        if (Input.GetKeyDown(KeyCode.W))
        {
            // 처음이라면 가이드2 화면 뜨게
            if (!isGuide2)
            {
                GuideText2.SetActive(true);
                isGuide2 = true;
            }

            // 체크 중일 때
            if (st2_game == ST2GAME.CHECK)
            {
                // 사운드 버튼 활성화
                LRButtonUI.SetActive(true);
                // 카메라 회전 (위로)
                s2camera.RotateW();
                Debug.Log("위를 쳐다봅니다.");

                // 쳐다보는 상태로 전환
                st2_game = ST2GAME.LOOK;
            }
        }
        // S키 입력 시
        if (Input.GetKeyDown(KeyCode.S))
        {
            // 로봇 쳐다보는 상태일 때
            if (st2_game == ST2GAME.LOOK)
            {
                // 사운드 버튼 비활성화
                LRButtonUI.SetActive(false);
                // 카메라 회전 (아래로)
                s2camera.RotateS();

                Debug.Log("작업으로 돌아갑니다.");
                // 체크 상태로 전환
                st2_game = ST2GAME.CHECK;
            }
        }

         // 2. 상태 체크
         switch (st2_game)
        {
         case ST2GAME.START:
             // 시작
             StartPlaying();
             break;
         case ST2GAME.WAIT:
             // 대기 상태
             break;
         case ST2GAME.CHECK:
             // 진행 중
             Check();
             break;
         case ST2GAME.LOOK:
             // 로봇 쳐다보는 중
             break;
         case ST2GAME.DEAD:
             // 사망할시 게임 오버
             // 로드 시 게임 시작 처음으로 돌아감
             DeadAnim();
             break;
         case ST2GAME.CLEAR:
             // 스테이지1 클리어
             Stage2Clear();
             break;
        }
    }

    void Check()
    {
        // 카드 6개가 모두 클릭되었으면
        if(countCard >= 6)
        {
            // 6개 모두 정답이면 스테이지2 클리어
            if (countAnswer >= 6)
                st2_game = ST2GAME.CLEAR;
            // 한개라도 아니면 재시작
            else
            {
                StartCoroutine("ReStart");
                st2_game = ST2GAME.WAIT;
            }
        }
    }

    IEnumerator ReStart()
    {
        Debug.Log("틀렸습니다. 처음부터 다시 시작합니다.");

        yield return new WaitForSeconds(2.0f);  // 2초 후 다시 시작

        // 카드 자식 오브젝트 1-6까지 모두 가져와서 초기화
        for (int i = 1; i <= 6; i++)
        {
            Stage2GameUI.transform.GetChild(i).gameObject.SendMessage("ResetCard");
            Stage2GameUI.transform.GetChild(i).gameObject.GetComponent<Card2>().StartCoroutine("Random");
        }
        // 다시 시작
        st2_game = ST2GAME.START;
    }

    void ResetStage2()
    {
        // 초기화
        countCard = 0;
        countAnswer = 0;
    }

    void StartPlaying()
    {
        // 초기화
        ResetStage2();

        // 체크 상태로 전환
        st2_game = ST2GAME.CHECK;
    }

    void DeadAnim()
    {
        Debug.Log("Game Over!");
        // 스테이지2 게임 UI 지우기
        Stage2GameUI.SetActive(false);

        // 사망 애니메이션 (카메라)
        s2camera.AnimPlay();

        // 대기 상태로 전환
        st2_game = ST2GAME.WAIT;
    }

    // 사망 애니메이션이 끝나면 실행됨
    public void Dead()
    {
        // 카드 초기화 (게임 UI 비활성화하기 전에 해줘야 하기 때문에 제일 먼저 씀)
        for (int i = 1; i <= 6; i++)
        {
            Stage2GameUI.transform.GetChild(i).gameObject.GetComponent<Card2>().ResetCard();
        }
        // 버튼 쿨타임 초기화
        SBManagerL.ResetButton();
        SBManagerR.ResetButton();

        Debug.Log("죽었습니다!");

        // 스테이지2 매니저 사망 상태로 전환
        Stage2Manager.stage2 = Stage2Manager.STAGE2.DEAD;

        // 스테이지2 게임 비활성화
        gameObject.SetActive(false);
    }

    void Stage2Clear()
    {
        // Clear 코루틴 함수 시작
        StartCoroutine("Clear");
        // 대기 상태로 전환
        st2_game = ST2GAME.WAIT;
    }

    IEnumerator Clear()
    {
        // 카드 이미지 비활성화
        for (int i = 1; i <= 6; i++)
        {
            Stage2GameUI.transform.GetChild(i).gameObject.SetActive(false);
        }
        // 성공 애니메이션
        Stage2GameUI.transform.Find("AnswerImg").gameObject.SetActive(true);
        // 성공 텍스트 활성화
        GameObject.Find("GameUI").transform.Find("ClearUI").gameObject.SetActive(true);


        yield return new WaitForSeconds(3.0f);  // 3초 기다린 후

        // 성공 텍스트 비활성화
        GameObject.Find("GameUI").transform.Find("ClearUI").gameObject.SetActive(false);
        // Start2 매니저 클리어 상태로 전환
        Stage2Manager.stage2 = Stage2Manager.STAGE2.CLEAR;

        // 게임 오브젝트 비활성화
        gameObject.SetActive(false);
    }
}
