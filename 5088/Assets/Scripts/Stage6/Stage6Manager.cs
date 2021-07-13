using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Manager : MonoBehaviour
{
    public Hall2InteractiveObject isStagestart; // 홀2상호작용오브젝트에서 게임 시작할지 판단 받아오기
    public ProgressBar barChecker; // 진행바 체크
    public GameObject mainCamera; // 메인 카메라
    public GameObject Camera6; // 스테이지6 카메라
    public GameObject RobotDeath; // 사망 애니메이션 로봇
    public GameObject ElevatorDoor;

    public GameObject gameOverUI; // 게임오버 메뉴
    public GameObject Stage6GameManager; // 스테이지6 게임 매니저 활성화
    public GameObject GuideText; // 가이드 텍스트
    public GameObject ProgressBar; // 진행바
    public GameObject S6GUI;       // 게임 UI
    public GameObject clearUI;       // 클리어 UI
    public Animator camera6Animator;
    public Animator GUI6Animator;
    public Animator DeathRobot;

    public AudioSource audioSource;
    public AudioSource bgm;

    public bool isDead = false;
    public bool s6_5 = false;
    public int ccount = 0;

    public string status; // 게임 진행 상태
    private void Awake()
    {
        
    }
    private void Start()
    {
        
    }

    void Update()
    {
        if (status == "GUIDE")
        {
            GuideStatus();
            
        }
        else if (status == "READY")
        {
            if (!bgm.mute)
            {
                bgm.Play();
            }
            else
                bgm.mute = false;

            status = "PLAY";
        }
        else if(status == "PLAY")
        {
            
        }
        else if(status == "DEAD")
        {
            bgm.mute = true;
            DeadStatus();
        }
        else if(status == "CLEAR")
        {
            
            bgm.mute = true;
            bgm.Stop();
            ClearStatus();
        }
    }

    

    public void GuideStatus()
    {
        S6GUI.SetActive(true); // 스테이지6 인터페이스 활성화
        GuideText.SetActive(true); // 가이드 텍스트

        // Space키 눌러지면
        if (Input.GetKeyDown(KeyCode.Space))
            {
                GuideText.SetActive(false); // 가이드 텍스트 내리기
                ProgressBar.SetActive(true); // 진행바 활성화
                GUI6Animator.SetInteger("GUIcheck", 1); // 스테이지6 인터페이스 올라오기

            // 스테이지6 게임 활성화
                Stage6GameManager.SetActive(true);
                status = "READY"; // 상태 PLAY로 변경
                barChecker.btnCheck = false;
                Debug.Log("스테이지6 게임 시작되었습니다.");

                
            }
    }

    public void PlayStatus()
    {

    }
        

            


    public void DeadStatus()
    {

        RobotDeath.SetActive(true);
        
        
        GUI6Animator.SetInteger("GUIcheck", 2); // 스테이지6 인터페이스 내리기

        // 스테이지6 GUI 비활성화
        //S6GUI.SetActive(false);

        StartCoroutine("Death");

        

    }

    IEnumerator Death()
    {
        if (isDead == false)
        {
            camera6Animator.SetTrigger("isDead");
            audioSource.Play();
            DeathRobot.SetTrigger("isRobotDeath");
            isDead = true;
            yield return new WaitForSeconds(0.05f);
            // 죽으면 게임오버 메뉴 활성화
            gameOverUI.SetActive(true);
            
            // 스테이지6게임매니저 비활성화
            //Stage6GameManager.SetActive(false);
            RobotDeath.SetActive(false);
            
        }
        
    }

    public void ClearStatus()
    {
        if (ccount == 0)
        {
            s6_5 = true;
            ccount = 1;
        }
        
        S6GUI.SetActive(false);
        // 비활성화 시켰던 플레이어 활성화
        // 비활성화 시켰던 플레이어 카메라 활성화
        mainCamera.SetActive(true);
        // 활성화 시켰던 스테이지6 카메라 비활성화
        Camera6.SetActive(false);
        // 엘레베이터로 갈 수 있는 상호작용 지역 활성화
        ElevatorDoor.SetActive(true);
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


        Debug.Log("스테이지6 완료");
        Debug.Log("게임모드에서 자유이동모드로 변경되었습니다.");
        
        // 플레이어 자유모드로 변경
        AllStageManager.p_state = AllStageManager.PLAYER_STATE.FREE;

        // 스테이지6 비활성화
        gameObject.SetActive(false);
    }
}
