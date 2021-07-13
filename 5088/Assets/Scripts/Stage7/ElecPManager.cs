using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElecPManager : MonoBehaviour
{
    // 전기 패턴 매니저
    public Stage7Manager statusChecker;
    public Stage7GameManager isRandNum;

    public GameObject ElecGUI; //GUI 비활성화

    public Animator Energy;
    public Animator eGUI;
    public Animator GUI;

    public float setTime = 10f;
    public Text countdownText;

    public AudioSource audioSource; // 전용 사운드

    public int clickCount = 0; // 몇 번 버튼을 클릭 했는가

    public string ElecStatus = "WAIT";

    public bool timeOut = false;

    void Start()
    {
        countdownText.text = setTime.ToString();
    }

    void Update()
    {
        if (ElecStatus == "READY")
        {
            audioSource.Play();
            ElecStatus = "PLAY";
        }

        else if (ElecStatus == "PLAY")
        {

            BarActive();
            Countdown(); // 카운트 다운
            ClearCheck();
            DeadCheck();
            countdownText.text = Mathf.Round(setTime).ToString();

        }
        else if (ElecStatus == "DEAD")
        {
            audioSource.Stop();
            eGUI.SetInteger("GUIcheck", 2); // 인터페이스 내리기
            GUI.SetInteger("GUIcheck", 1); // 다시 기본 인터페이스 올라오기
            Reset();
            statusChecker.status = "DEAD";
            ElecGUI.SetActive(false);
        }
        else if (ElecStatus == "CLEAR")
        {
            audioSource.Stop();
            eGUI.SetInteger("GUIcheck", 2); // 인터페이스 내리기
            GUI.SetInteger("GUIcheck", 1); // 다시 기본 인터페이스 올라오기
            isRandNum.isRandNum = false;
            ElecGUI.SetActive(false);
            Reset();
        }
    }


    // 버튼 상호작용
    public void ClickBtn1()
    {
        clickCount++;
    }




    // 애니메이션 작동
    public void BarActive()
    {
        Energy.SetInteger("ClickCount", clickCount);
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
            ElecStatus = "DEAD";
        }
    }

    // 승리 조건 체크
    public void ClearCheck()
    {
        // 시간초과 아니고 각 클릭 카운트가 모두 12이상일 경우 승리
        if (!timeOut && clickCount >= 42)
        {
            ElecStatus = "CLEAR"; // 클리어 상태
        }
    }

    public void Reset()
    {
        clickCount = 0;
        setTime = 9f;
        Energy.Rebind();
        ElecStatus = "WAIT";
        timeOut = false;
    }
}
