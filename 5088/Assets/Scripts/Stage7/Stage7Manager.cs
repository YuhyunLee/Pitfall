using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7Manager : MonoBehaviour
{
    public HackBar barChecker; // 진행바 체크
    public FadeEffect fade; // 페이드 효과

    public GameObject mainCamera; // 메인 카메라
    public GameObject Camera7; // 스테이지7 카메라
    public GameObject ExitArea; // 승리시 활성화되는 탈출구역
    public GameObject panel; // 승리시 활성화되는 탈출구역

    public GameObject gameOverUI; // 게임오버 메뉴
    public GameObject Stage7GameManager; // 스테이지6 게임 매니저 활성화
    public GameObject GuideText; // 가이드 텍스트
    public GameObject S7GUI;       // 게임 메인 UI
    public GameObject clearUI;       // 클리어 UI

    public Animator camera7Animator; // 카메라7의 애니메이터
    public Animator GUI7Animator; // 스테이지7의 GUI 애니메이터

    public AudioSource deadSound;
    public AudioSource bossBGM;

    public bool s7_2 = false;
    public bool s7_4 = false;


    public bool isDead = false;

    public string status; // 게임 진행 상태


    void Update()
    {
        if (status == "GUIDE")
        {
            GuideStatus();

        }
        else if (status == "READY")
        {
            if(bossBGM.mute == false)
                bossBGM.Play();
            else
                bossBGM.mute = false;
            status = "PLAY";
        }
        else if (status == "PLAY")
        {

        }
        else if (status == "DEAD")
        {
            DeadStatus();
            bossBGM.mute = true;
        }
        else if (status == "CLEAR")
        {
            ClearStatus();
            bossBGM.mute = true;
        }
    }



    public void GuideStatus()
    {
        S7GUI.SetActive(true); // 스테이지6 인터페이스 활성화
        GuideText.SetActive(true); // 가이드 텍스트

        // Space키 눌러지면
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GuideText.SetActive(false); // 가이드 텍스트 내리기
            GUI7Animator.SetInteger("GUIcheck", 1); // 스테이지6 인터페이스 올라오기

            // 스테이지6 게임 활성화
            Stage7GameManager.SetActive(true);
            status = "READY"; // 상태 READY로 변경
            barChecker.btnCheck = false; // 진행바 움직이십쇼
            s7_2 = true;

            Debug.Log("스테이지7 게임 시작되었습니다.");


        }
    }

    public void PlayStatus()
    {

    }





    public void DeadStatus()
    {
        panel.SetActive(true);

        GUI7Animator.SetInteger("GUIcheck", 2); // 스테이지6 인터페이스 내리기

        // 스테이지7 GUI 비활성화
        S7GUI.SetActive(false);

        StartCoroutine("Death");



    }

    IEnumerator Death()
    {
        if (isDead == false)
        {
            fade.FadeOut(); // 죽었을 때 재생되는 애니메이션
            if (deadSound.mute == false)
                deadSound.Play();
            else
                deadSound.mute = false;
            isDead = true;
            yield return new WaitForSeconds(0.05f);
            deadSound.mute = false;
            // 죽으면 게임오버 메뉴 활성화
            gameOverUI.SetActive(true);
            // 스테이지6게임매니저 비활성화
            //Stage7GameManager.SetActive(false);

        }

    }

    public void ClearStatus()
    {
        S7GUI.SetActive(false);
        // 비활성화 시켰던 플레이어 카메라 활성화
        mainCamera.SetActive(true);
        // 활성화 시켰던 스테이지6 카메라 비활성화
        Camera7.SetActive(false);
        // 엘레베이터로 갈 수 있는 상호작용 지역 활성화
        ExitArea.SetActive(true);
        // 코루틴 함수 시작
        StartCoroutine("Clear");
        // 

    }

    IEnumerator Clear()
    {

        // 성공 텍스트 활성화
        clearUI.SetActive(true);

        yield return new WaitForSeconds(3.0f);  // 3초 기다린 후

        // 성공 텍스트 비활성화
        clearUI.SetActive(false);
        s7_4 = true;


        Debug.Log("스테이지7 완료. 고생했다.");
        Debug.Log("게임모드에서 자유이동모드로 변경되었습니다.");

        // 플레이어 자유모드로 변경
        AllStageManager.p_state = AllStageManager.PLAYER_STATE.FREE;

        // 스테이지6 비활성화
        gameObject.SetActive(false);
    }
}
