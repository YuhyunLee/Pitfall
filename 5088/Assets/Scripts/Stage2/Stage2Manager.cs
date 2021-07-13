using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Manager : MonoBehaviour
{

    public bool s2_4 = false;
    public enum STAGE2
    {
        WAIT, MAINGAME, DEAD, CLEAR
    };
    static public STAGE2 stage2 = STAGE2.WAIT;      // 처음에는 대기 상태

    void Update()
    {
        switch (stage2)
        {
            case STAGE2.WAIT:
                // 대기 상태
                break;
            case STAGE2.MAINGAME:
                // 메인 게임
                MainGameStart();
                break;
            case STAGE2.DEAD:
                // 사망 시 다시 로드
                Dead();
                break;
            case STAGE2.CLEAR:
                // 스테이지2 클리어
                Stage2Clear();
                break;
        }
    }

    public void GameStart()
    {
        // 메인 게임 상태로 전환
        stage2 = STAGE2.MAINGAME;
    }

    void MainGameStart()
    {
        // 처음이라면
        if(!AllStageManager.isLoad)
        {
            // 게임 시작 안내 텍스트
            GameObject.Find("GameUI").transform.Find("SpaceKeyUI").gameObject.SetActive(true);

            // Space키 눌러지면
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 안내 텍스트 꺼주고
                GameObject.Find("GameUI").transform.Find("SpaceKeyUI").gameObject.SetActive(false);

                // 스테이지2 게임 활성화
                gameObject.transform.Find("Stage2GameManager").gameObject.SetActive(true);
                Debug.Log("스테이지2 게임 시작되었습니다.");

                // 대기 상태로 전환
                stage2 = STAGE2.WAIT;
            }
        }
        // 로드된 데이터라면
        else
        {
            // 스테이지2 게임 활성화
            gameObject.transform.Find("Stage2GameManager").gameObject.SetActive(true);
            Debug.Log("스테이지2 게임 시작되었습니다.");

            // 대기 상태로 전환
            stage2 = STAGE2.WAIT;
        }
    }

    public void Dead()
    {
        // 스테이지 매니저 로드 상태로 전환
        AllStageManager.stage = AllStageManager.STAGE.GAMEOVER;
        // 대기 상태로 전환
        stage2 = STAGE2.WAIT;
    }

    public void Stage2Clear()
    {
        s2_4 = true;
        Debug.Log("스테이지2 완료");
        Debug.Log("게임모드에서 자유이동모드로 변경되었습니다.");

        // 클리어 단계 2로 변경
        AllStageManager.clearStage = 2;
        // 로드 false로
        AllStageManager.isLoad = false;
        // 플레이어 자유모드로 변경
        AllStageManager.p_state = AllStageManager.PLAYER_STATE.FREE;

        // 스테이지2 비활성화
        gameObject.SetActive(false);
    }
}
