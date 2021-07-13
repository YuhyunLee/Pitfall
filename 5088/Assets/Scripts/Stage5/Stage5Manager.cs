using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Manager : MonoBehaviour
{
    public bool s5_1 = false;
    public bool s5_3 = false;
    public int ccount = 0;
    public enum STAGE5
    {
        WAIT, MAINGAME, DEAD, CLEAR
    };
    static public STAGE5 stage5 = STAGE5.WAIT;      // 처음에는 대기 상태

    void Update()
    {
        switch (stage5)
        {
            case STAGE5.WAIT:
                // 대기 상태
                
                break;
            case STAGE5.MAINGAME:
                // 메인 게임
                MainGameStart();
                break;
            case STAGE5.DEAD:
                // 사망 시 다시 로드
                Dead();
                break;
            case STAGE5.CLEAR:
                // 스테이지4 클리어
                
                Stage5Clear();
                break;
        }
    }

    public void GameStart()
    {
        // 메인 게임 상태로 전환
        stage5 = STAGE5.MAINGAME;
    }

    void MainGameStart()
    {
        if (ccount == 0)
        {
            s5_1 = true;
            ccount = 1;
        }
        
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

                // 스테이지5 게임 활성화
                gameObject.transform.Find("S5GameManager").gameObject.SetActive(true);
                Debug.Log("스테이지5 게임 시작되었습니다.");

                // 대기 상태로 전환
                stage5 = STAGE5.WAIT;
            }
        }
        // 로드된 데이터라면
        else
        {
            // 스테이지5 게임 활성화
            gameObject.transform.Find("S5GameManager").gameObject.SetActive(true);
            Debug.Log("스테이지5 게임 시작되었습니다.");

            // 대기 상태로 전환
            stage5 = STAGE5.WAIT;
        }
    }

    public void Dead()
    {
        // 스테이지 매니저 로드 상태로 전환
        AllStageManager.stage = AllStageManager.STAGE.GAMEOVER;
        // 대기 상태로 전환
        stage5 = STAGE5.WAIT;
    }

    public void Stage5Clear()
    {
        if (ccount == 1)
        {
            s5_3 = true;
            ccount = 2;
        }
        // 코루틴 함수 시작
        StartCoroutine("Clear");
        // 대기 상태로 전환
        stage5 = STAGE5.WAIT;
    }

    IEnumerator Clear()
    {
        // 성공 텍스트 활성화
        GameObject.Find("GameUI").transform.Find("ClearUI").gameObject.SetActive(true);

        yield return new WaitForSeconds(3.0f);  // 3초 기다린 후

        // 성공 텍스트 비활성화
        GameObject.Find("GameUI").transform.Find("ClearUI").gameObject.SetActive(false);


        Debug.Log("스테이지5 완료");
        Debug.Log("게임모드에서 자유이동모드로 변경되었습니다.");

        // 클리어 단계 5로 변경
        AllStageManager.clearStage = 5;
        // 로드 false로
        AllStageManager.isLoad = false;
        // 플레이어 자유모드로 변경
        AllStageManager.p_state = AllStageManager.PLAYER_STATE.FREE;

        // 스테이지5 비활성화
        gameObject.SetActive(false);
    }
}
