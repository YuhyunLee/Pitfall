using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7GameManager : MonoBehaviour
{
    public Stage7Manager statusChecker; // 상태 체크
    public HackBar barChecker; // 진행바 체크
    public SoundPManager soundPManager; // 사운드 패턴 매니저
    public ElecPManager elecPManager; // 전기 패턴 매니저
    public GunPManager gunPManager; // 총 패턴 매니저
    public PoisonPManager poisonPManager; // 독 패턴 매니저

    public GameObject SoundGUI; // 소리패턴ui활성화
    public GameObject elecGUI; // 전기패턴ui활성화
    public GameObject gunGUI; // 총패턴ui활성화
    public GameObject poisonGUI; // 독패턴ui활성화


    public Animator camera7Animator; // 카메라 애니메이터
    public Animator GUI7Animator; // GUI애니메이터
    public Animator sGUIAnimator; // soundGUI애니메이터
    public Animator eGUIAnimator; // electricGUI애니메이터
    public Animator gGUIAnimator; // gunGUI애니메이터
    public Animator pGUIAnimator; // gunGUI애니메이터

    public int upMove = 0;// 위쪽 보기 키 누를 시 애니메이션 변수 변동
    public bool isRandNum = false; // random 패턴을 골랐는가

    public float delayTime = 10f;
    public float nextTime = 0f;


    private int randNum;
    

    private void OnEnable()
    {
        


    }

    private void Start()
    {
        
        
        
        
    }

    private void Update()
    {
        if (statusChecker.status == "PLAY")
        {
            if (nextTime >= delayTime)
            {
                rand();
                nextTime = 0;
            }
            else
            {
                nextTime += Time.deltaTime;
            }
            
            UpLook();
        }
        

        //프로그레스바의 현재 상태가 160이상이면 승리
        if (statusChecker.status != "DEAD" && barChecker.current >= 160)
        {
            Stage7Win();
        }
    }

    // 랜덤 수 구하고 해당 게임 호출
    public void rand()
    {
        if (!isRandNum)
        {
            randNum = Random.Range(1, 5);
            isRandNum = true;
            Debug.Log("랜덤수 : " + randNum);
            switch(randNum)
            {
                case 1:
                    // 소리 패턴 호출
                    SoundGUI.SetActive(true);
                    soundPManager.SoundStatus = "READY";
                    sGUIAnimator.SetInteger("GUIcheck", 1);
                    GUI7Animator.SetInteger("GUIcheck", 2);
                    Debug.Log("소리패턴");
                    break;
                case 2:
                    // 전기 패턴 호출
                    elecGUI.SetActive(true);
                    elecPManager.ElecStatus = "READY";
                    eGUIAnimator.SetInteger("GUIcheck", 1);
                    GUI7Animator.SetInteger("GUIcheck", 2);
                    Debug.Log("전기패턴");
                    break;
                case 3:
                    // 총 패턴 호출
                    gunGUI.SetActive(true);
                    gunPManager.GunStatus = "READY";
                    gGUIAnimator.SetInteger("GUIcheck", 1);
                    GUI7Animator.SetInteger("GUIcheck", 2);
                    Debug.Log("총패턴");
                    break;
                case 4:
                    // 독 패턴 호출
                    poisonGUI.SetActive(true);
                    poisonPManager.PoisonStatus = "READY";
                    pGUIAnimator.SetInteger("GUIcheck", 1);
                    GUI7Animator.SetInteger("GUIcheck", 2);
                    Debug.Log("독패턴");
                    break;
            }
        }
    }

    public void Stage7Win()
    {
        statusChecker.status = "CLEAR";
        SoundGUI.SetActive(false);
        elecGUI.SetActive(false);
        gunGUI.SetActive(false);
        poisonGUI.SetActive(false);
    }

    // 위쪽 보기 키를 눌렀을 때
    public void UpLook()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (upMove == 0) // idle 애니메이션 상태일 때 누르면 위쪽 보기
            {
                camera7Animator.SetInteger("C7Status", 1);
                upMove++;
                GUI7Animator.SetInteger("GUIcheck", 2);
            }
            else if (upMove == 1) // 위쪽을 보고 있는 상태일 때 누르면 돌아오기
            {
                camera7Animator.SetInteger("C7Status", 2);
                upMove--;
                GUI7Animator.SetInteger("GUIcheck", 3);
                Invoke("upIdle", 0.5f);
            }

        }
    }


    // 딜레이 주고 idle 상태로 복귀
    public void upIdle()
    {
        camera7Animator.SetInteger("C7Status", 0);
        Debug.Log("위쪽 초기화");
    }



}
