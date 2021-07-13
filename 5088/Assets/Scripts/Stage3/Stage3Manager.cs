using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Manager : MonoBehaviour
{
    public enum STAGE3
    {
        WAIT, MAINGAME, DEAD, CLEAR
    };
    static public STAGE3 stage3 = STAGE3.WAIT;      // 처음에는 대기 상태

    void Update()
    {
        switch (stage3)
        {
            case STAGE3.WAIT:
                // 대기 상태
                break;
            case STAGE3.MAINGAME:
                // 메인 게임
                MainGameStart();
                break;
            case STAGE3.DEAD:
                // 사망 시 다시 로드
                Dead();
                break;
            case STAGE3.CLEAR:
                // 스테이지3 클리어
                Stage3Clear();
                break;
        }
    }

    public void GameStart()
    {
        // 메인 게임 상태로 전환
        stage3 = STAGE3.MAINGAME;
    }

    void MainGameStart()
    {
        // 처음이라면
        if (!AllStageManager.isLoad)
        {
            // 게임 시작 안내 텍스트
            GameObject.Find("GameUI").transform.Find("SpaceKeyUI").gameObject.SetActive(true);

            // Space키 눌러지면
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 안내 텍스트 꺼주고
                GameObject.Find("GameUI").transform.Find("SpaceKeyUI").gameObject.SetActive(false);

                // 스테이지3 게임 활성화
                gameObject.transform.Find("S3MainGameManager").gameObject.SetActive(true);
                Debug.Log("스테이지3 게임 시작되었습니다.");

                // 대기 상태로 전환
                stage3 = STAGE3.WAIT;
            }
        }
        // 로드된 데이터라면
        else
        {
            // 스테이지3 게임 활성화
            gameObject.transform.Find("S3MainGameManager").gameObject.SetActive(true);
            Debug.Log("스테이지3 게임 시작되었습니다.");

            // 대기 상태로 전환
            stage3 = STAGE3.WAIT;
        }
    }

    public void Dead()
    {
        // 스테이지 매니저 로드 상태로 전환
        AllStageManager.stage = AllStageManager.STAGE.GAMEOVER;
        // 대기 상태로 전환
        stage3 = STAGE3.WAIT;
    }

    public void Stage3Clear()
    {
        // 코루틴 함수 시작
        StartCoroutine("Clear");
        // 대기 상태로 전환
        stage3 = STAGE3.WAIT;
    }

    IEnumerator Clear()
    {
        // 성공 텍스트 활성화
        GameObject.Find("GameUI").transform.Find("ClearUI").gameObject.SetActive(true);

        yield return new WaitForSeconds(3.0f);  // 3초 기다린 후

        // 성공 텍스트 비활성화
        GameObject.Find("GameUI").transform.Find("ClearUI").gameObject.SetActive(false);

        Debug.Log("스테이지3 완료");
        Debug.Log("게임모드에서 자유이동모드로 변경되었습니다.");

        // 클리어 단계 3으로 변경
        AllStageManager.clearStage = 3;
        // 로드 false로
        AllStageManager.isLoad = false;
        // 플레이어 자유모드로 변경
        AllStageManager.p_state = AllStageManager.PLAYER_STATE.FREE;

        // 스테이지3 비활성화
        gameObject.SetActive(false);
    }
}
