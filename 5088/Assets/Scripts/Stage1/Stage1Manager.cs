using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Manager : MonoBehaviour
{
    public bool s1_11 = false;
    public enum STAGE1
    {
        CHECK, WAIT, IMGGAME, MAINGAME, DEAD, CLEAR
    };
    static public STAGE1 stage1 = STAGE1.WAIT;      // 처음에는 대기 상태


    // Update is called once per frame
    void Update()
    {
        switch (stage1)
        {
            case STAGE1.WAIT:
                // 대기 상태
                break;
            case STAGE1.IMGGAME:
                // 이미지 게임
                ImgGameStart();
                break;
            case STAGE1.MAINGAME:
                // 스테이지1 메인 게임
                MainGameStart();
                break;
            case STAGE1.DEAD:
                // 사망 시 메인게임부터 다시 로드
                Dead();
                break;
            case STAGE1.CLEAR:
                // 스테이지1 클리어
                Stage1Clear();
                break;
        }

    }

    public void GameStart()
    {
        // 로드된 데이터면 무조건 메인게임부터
        if(AllStageManager.isLoad)
        {
            stage1 = STAGE1.MAINGAME;
        }
        // 처음하는거라면 이미지 게임 시작
        else
        {
            // 이미지 게임 시작
            stage1 = STAGE1.IMGGAME;
        }
    }

    public void ImgGameStart()
    {
        // 게임 시작 안내 텍스트
        GameObject.Find("GameUI").transform.Find("SpaceKeyUI").gameObject.SetActive(true);

        // Space키 눌러지면
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 안내 텍스트 꺼주고
            GameObject.Find("GameUI").transform.Find("SpaceKeyUI").gameObject.SetActive(false);

            // 이미지 게임 활성화
            gameObject.transform.Find("ImageGameManager").gameObject.SetActive(true);
            Debug.Log("스테이지1 이미지 게임 시작되었습니다.");

            // 대기 상태로 전환
            stage1 = STAGE1.WAIT;
        }
    }

    void MainGameStart()
    {
        // 메인 게임 활성화
        gameObject.transform.Find("S1MainGameManager").gameObject.SetActive(true);
        Debug.Log("스테이지1 메인 게임 시작되었습니다.");

        // 대기 상태로 전환
        stage1 = STAGE1.WAIT;
    }

    public void Dead()
    {
        // 스테이지 매니저 게임오버 상태로 전환
        AllStageManager.stage = AllStageManager.STAGE.GAMEOVER;
        // 대기 상태로 전환
        stage1 = STAGE1.WAIT;
    }

    public void Stage1Clear()
    {
        s1_11 = true;
        // 코루틴 함수 시작
        StartCoroutine("Clear");
        // 대기 상태로 전환
        stage1 = STAGE1.WAIT;
    }

    IEnumerator Clear()
    {
        // 성공 텍스트 활성화
        GameObject.Find("GameUI").transform.Find("ClearUI").gameObject.SetActive(true);

        yield return new WaitForSeconds(3.0f);  // 3초 기다린 후

        // 성공 텍스트 비활성화
        GameObject.Find("GameUI").transform.Find("ClearUI").gameObject.SetActive(false);

        Debug.Log("스테이지1 완료");
        Debug.Log("게임모드에서 자유이동모드로 변경되었습니다.");

        // 클리어 단계 1로 변경
        AllStageManager.clearStage = 1;
        // 로드 false로
        AllStageManager.isLoad = false;
        // 플레이어 자유모드로 변경
        AllStageManager.p_state = AllStageManager.PLAYER_STATE.FREE;

        // 스테이지1 비활성화
        gameObject.SetActive(false);
    }
}
