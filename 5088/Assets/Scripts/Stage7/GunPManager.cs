using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPManager : MonoBehaviour
{
    // 전기 패턴 매니저
    public Stage7Manager statusChecker;
    public Stage7GameManager isRandNum;

    public GameObject GunGUI; //GUI 비활성화

    public Animator Mag;
    public Animator gGUI;
    public Animator GUI;

    public float setTime = 10f;
    public Text countdownText;

    

    public int clickCount = 0; // 몇 번 버튼을 클릭 했는가

    public string GunStatus = "WAIT";

    public bool timeOut = false;

    void Start()
    {
        countdownText.text = setTime.ToString();
    }

    void Update()
    {
        if (GunStatus == "READY")
        {
            GunStatus = "PLAY";
        }

        else if (GunStatus == "PLAY")
        {

            BarActive();
            Countdown(); // 카운트 다운
            ClearCheck();
            DeadCheck();
            countdownText.text = Mathf.Round(setTime).ToString();

        }
        else if (GunStatus == "DEAD")
        {
            gGUI.SetInteger("GUIcheck", 2); // 인터페이스 내리기
            GUI.SetInteger("GUIcheck", 1); // 다시 기본 인터페이스 올라오기
            Reset();
            statusChecker.status = "DEAD";
            GunGUI.SetActive(false);
        }
        else if (GunStatus == "CLEAR")
        {
            gGUI.SetInteger("GUIcheck", 2); // 인터페이스 내리기
            GUI.SetInteger("GUIcheck", 1); // 다시 기본 인터페이스 올라오기
            GunGUI.SetActive(false);
            isRandNum.isRandNum = false;
            Reset();
        }
    }


    // 버튼 상호작용
    




    // 애니메이션 작동
    public void BarActive()
    {
        Mag.SetInteger("Count", clickCount);
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
            GunStatus = "DEAD";
        }
    }

    // 승리 조건 체크
    public void ClearCheck()
    {
        // 시간초과 아니고 각 클릭 카운트가 모두 7이상일 경우 승리
        if (!timeOut && clickCount >= 7)
        {
            GunStatus = "CLEAR"; // 클리어 상태
        }
    }

    public void Reset()
    {
        clickCount = 0;
        setTime = 15f;
        Mag.Rebind();
        GunStatus = "WAIT";
        timeOut = false;
    }
}
