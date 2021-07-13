using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonPManager : MonoBehaviour
{
    // 전기 패턴 매니저
    public Stage7Manager statusChecker;
    public Stage7GameManager isRandNum;

    public GameObject PoisonGUI; //GUI 비활성화

    public Animator Valve1;
    public Animator Valve2;
    public Animator Valve3;
    public Animator pGUI;
    public Animator GUI;

    public float setTime = 10f;
    public Text countdownText;


    public bool v1 = false; // 눌렸는지 확인
    public bool v2 = false; // 눌렸는지 확인
    public bool v3 = false; // 눌렸는지 확인

    public string PoisonStatus = "WAIT";

    public bool timeOut = false;

    void Start()
    {
        countdownText.text = setTime.ToString();
    }

    void Update()
    {
        if (PoisonStatus == "READY")
        {
            PoisonStatus = "PLAY";
        }

        else if (PoisonStatus == "PLAY")
        {

            BarActive1();
            BarActive2();
            BarActive3();
            Countdown(); // 카운트 다운
            ClearCheck();
            DeadCheck();
            countdownText.text = Mathf.Round(setTime).ToString();

        }
        else if (PoisonStatus == "DEAD")
        {
            pGUI.SetInteger("GUIcheck", 2); // 인터페이스 내리기
            GUI.SetInteger("GUIcheck", 1); // 다시 기본 인터페이스 올라오기
            Reset();
            statusChecker.status = "DEAD";
            PoisonGUI.SetActive(false);
        }
        else if (PoisonStatus == "CLEAR")
        {
            pGUI.SetInteger("GUIcheck", 2); // 인터페이스 내리기
            GUI.SetInteger("GUIcheck", 1); // 다시 기본 인터페이스 올라오기
            isRandNum.isRandNum = false;
            PoisonGUI.SetActive(false);
            Reset();
        }
    }


    // 버튼 상호작용
    public void ClickBtn1()
    {
        v1 = true;
    }
    public void ClickBtn2()
    {
        v2 = true;
    }
    public void ClickBtn3()
    {
        v3 = true;
    }




    // 애니메이션 작동
    public void BarActive1()
    {
        Valve1.SetBool("Click", v1);
    }
    public void BarActive2()
    {
        Valve2.SetBool("Click", v2);
    }
    public void BarActive3()
    {
        Valve3.SetBool("Click", v3);
    }

    // 카운트 다운
    void Countdown()
    {
        if (setTime > 0)
        {
            setTime -= Time.deltaTime;
        }
        else if (setTime < 0)
        {
            timeOut = true;
        }

    }
    // 죽음 조건 체크
    public void DeadCheck()
    {
        if (timeOut)
        {
            PoisonStatus = "DEAD";
        }
    }

    // 승리 조건 체크
    public void ClearCheck()
    {
        // 시간초과 아니고 각 클릭 카운트가 모두 12이상일 경우 승리
        if (!timeOut && v1 && v2 && v3)
        {
            PoisonStatus = "CLEAR"; // 클리어 상태
        }
    }

    public void Reset()
    {
        v1 = false;
        v2 = false;
        v3 = false;
        setTime = 3f;
        Valve1.Rebind();
        Valve2.Rebind();
        Valve3.Rebind();
        PoisonStatus = "WAIT";
        timeOut = false;
    }
}
