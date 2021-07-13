using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage6GameManager : MonoBehaviour
{
    public Stage6Manager statusChecker;
    public ProgressBar barChecker;
    public Robot6Controller robotChecker;
    public GameObject player;

    public Animator camera6Animator;
    public Animator GUI6Animator;

    public int leftMove = 0; // 왼쪽 보기 키 누를 시 애니메이션 변수 변동
    public int rightMove = 0;// 오른쪽 보기 키 누를 시 애니메이션 변수 변동
    public int upMove = 0;// 위쪽 보기 키 누를 시 애니메이션 변수 변동
    public int backMove = 0;// 뒤쪽 보기 키 누를 시 애니메이션 변수 변동

    public bool backCheck = false;


    private void OnEnable()
    {
        

        
    }

    private void Start()
    {
        
        
    }

    private void Update()
    {
        LeftLook();
        RightLook();
        UpLook();
        BackLook();

        //프로그레스바의 현재 상태가 180이상이면 승리
        if (statusChecker.status != "DEAD" && barChecker.current >= 180)
        {
            Stage6Win();
            player.SetActive(true);
        }
    }

    public void Stage6Win()
    {
        statusChecker.status = "CLEAR";
    }

    public void Dead()
    {
        statusChecker.status = "DEAD";
    }
    

    // 왼쪽 보기 키를 눌렀을 때
    public void LeftLook()
    {
        if (Input.GetKeyDown(KeyCode.A)){
            if(leftMove == 0) // idle 애니메이션 상태일 때 누르면 왼쪽 보기
            {
                camera6Animator.SetInteger("LeftMove", 1);
                leftMove++;
                GUI6Animator.SetInteger("GUIcheck", 2);
            }
            else if(leftMove == 1) // 왼쪽을 보고 있는 상태일 때 누르면 돌아오기
            {
                camera6Animator.SetInteger("LeftMove", 2);
                leftMove--;
                GUI6Animator.SetInteger("GUIcheck", 1);
                Invoke("leftIdle", 1f);
            }

            
        }
    }

    // 오른쪽 보기 키를 눌렀을 때
    public void RightLook()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (rightMove == 0) // idle 애니메이션 상태일 때 누르면 왼쪽 보기
            {
                camera6Animator.SetInteger("RightMove", 1);
                rightMove++;
                GUI6Animator.SetInteger("GUIcheck", 2);
            }
            else if (rightMove == 1) // 왼쪽을 보고 있는 상태일 때 누르면 돌아오기
            {
                camera6Animator.SetInteger("RightMove", 2);
                rightMove--;
                GUI6Animator.SetInteger("GUIcheck", 1);
                Invoke("rightIdle", 1f);
            }


        }
    }

    // 위쪽 보기 키를 눌렀을 때
    public void UpLook()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (upMove == 0) // idle 애니메이션 상태일 때 누르면 왼쪽 보기
            {
                camera6Animator.SetInteger("UpMove", 1);
                upMove++;
                GUI6Animator.SetInteger("GUIcheck", 2);
            }
            else if (upMove == 1) // 왼쪽을 보고 있는 상태일 때 누르면 돌아오기
            {
                camera6Animator.SetInteger("UpMove", 2);
                upMove--;
                GUI6Animator.SetInteger("GUIcheck", 1);
                Invoke("upIdle", 1f);
            }

        }
    }

    // 뒤쪽 보기 키를 눌렀을 때
    public void BackLook()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (backMove == 0) // idle 애니메이션 상태일 때 누르면 왼쪽 보기
            {
                camera6Animator.SetInteger("BackMove", 1);
                backMove++;
                GUI6Animator.SetInteger("GUIcheck", 2);
                backCheck = true;
            }
            else if (backMove == 1) // 왼쪽을 보고 있는 상태일 때 누르면 돌아오기
            {
                camera6Animator.SetInteger("BackMove", 2);
                backMove--;
                GUI6Animator.SetInteger("GUIcheck", 1);
                backCheck = false;
                Invoke("backIdle", 1f);
            }


        }
    }

    // 딜레이 주고 idle 상태로 복귀
    public void leftIdle()
    {
        camera6Animator.SetInteger("LeftMove", 0);
        Debug.Log("왼쪽 초기화");
    }
    public void rightIdle()
    {
        camera6Animator.SetInteger("RightMove", 0);
        Debug.Log("오른쪽 초기화");
    }
    public void upIdle()
    {
        camera6Animator.SetInteger("UpMove", 0);
        Debug.Log("위쪽 초기화");
    }
    public void backIdle()
    {
        camera6Animator.SetInteger("BackMove", 0);
        Debug.Log("뒤쪽 초기화");
    }

    public void ResetState()
    {
        // 애니메이터 초기화
        camera6Animator.Rebind();
        GUI6Animator.Rebind();
    }

    



}
